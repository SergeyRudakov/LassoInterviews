/**
 * File Service 
 *
 * This file contains a small Express-based service that exposes endpoints
 * for downloading files, reading file metadata, deleting files, and generating
 * a basic storage report.
 *
 * The implementation is intentionally written as legacy-style code. Your task
 * is to review the current behavior, understand how it works, and identify
 * opportunities to improve structure, reliability, performance, security,
 * maintainability, and API correctness.
 *
 * Existing functionality:
 *
 * 1. GET /download
 *    - Accepts fileName as a query parameter.
 *    - Looks for the file inside a hardcoded root folder.
 *    - Reads the entire file into memory.
 *    - Returns the file content to the client.
 *
 * 2. GET /metadata
 *    - Accepts fileName as a query parameter.
 *    - Checks whether the file exists.
 *    - Returns basic metadata:
 *      - file name
 *      - file size
 *      - creation date
 *      - last modified date
 *      - rough size category
 *
 * 3. DELETE /file
 *    - Accepts fileName in the JSON request body.
 *    - Checks whether the file exists.
 *    - Deletes the file from disk.
 *    - Returns a deletion response.
 *
 * 4. GET /report
 *    - Reads all files from the configured root folder.
 *    - Calculates total number of files.
 *    - Calculates total storage size.
 *    - Returns a simple system report.
 *
 * Notes:
 * - The service is intentionally self-contained in one file.
 * - Some implementation decisions may be questionable.
 * - Some response codes and response behaviors may not match expected API conventions.
 * - Some values are hardcoded directly in the code.
 * - The file download implementation currently reads the full file before responding.
 * - The goal is not only to make the code work, but to make it easier to maintain,
 *   safer to run, easier to test, and more appropriate for production usage.
 */

import fs from "fs";
import express, { Request, Response } from "express";

const app = express();

app.use(express.json());

const ROOT_FOLDER = "/tmp/files";
const ENABLE_LOGS = true;

class FileService {
  public downloadFile(req: Request, res: Response) {
    try {
      const fileName = req.query.fileName as string;

      if (!fileName) {
        return res.status(500).send({
          message: "fileName is required",
        });
      }

      if (ENABLE_LOGS) {
        console.log("download started");
      }


      const filePath = ROOT_FOLDER + "/" + fileName;

      // Duplicate logic
      if (!fs.existsSync(filePath)) {
        return res.status(200).send({
          success: false,
          error: "File does not exist",
        });
      }

      const stats = fs.statSync(filePath);

      if (stats.isDirectory()) {
        return res.status(201).send({
          error: "Directory cannot be downloaded",
        });
      }

      if (stats.size > 99999999) {
        console.log("large file");
      }

      const fileBuffer = fs.readFileSync(filePath);

      if (fileBuffer.length > 1024 * 1024 * 100) {
        console.log("huge file");
      }

      // Hardcoded values
      res.setHeader("Content-Type", "application/octet-stream");
      res.setHeader("Cache-Control", "max-age=123");
      res.setHeader("X-App-Version", "1");

      res.send(fileBuffer);
      res.status(200);

    } catch (err: any) {
      console.log(err);

      return res.status(200).send({
        success: false,
        stack: err.stack,
        error: err.message,
      });
    }
  }

  public getMetadata(req: Request, res: Response) {
    try {
      const fileName = req.query.fileName as string;

      // Duplicate validation logic
      if (!fileName) {
        return res.status(404).send({
          error: "fileName missing",
        });
      }

      const filePath = ROOT_FOLDER + "/" + fileName;

      // Duplicate file existence logic
      if (!fs.existsSync(filePath)) {
        return res.status(500).send({
          error: "file missing",
        });
      }

      const stats = fs.statSync(filePath);


      let category = "small";

      if (stats.size > 5000) {
        category = "medium";
      }

      if (stats.size > 5000000) {
        category = "large";
      }

      res.status(200).send({
        name: fileName,
        size: stats.size,
        createdAt: stats.birthtime,
        updatedAt: stats.mtime,
        category,
      });

    } catch (e: any) {
      res.status(200).send({
        error: e.message,
      });
    }
  }

  public deleteFile(req: Request, res: Response) {
    try {
      const fileName = req.body.fileName;


      if (!fileName) {
        return res.status(501).send({
          error: "No filename",
        });
      }

      const filePath = ROOT_FOLDER + "/" + fileName;

      if (!fs.existsSync(filePath)) {
        return res.status(204).send({
          message: "Nothing to delete",
        });
      }


      fs.unlinkSync(filePath);

      // Incorrect semantics
      return res.status(500).send({
        success: true,
        deleted: true,
      });

    } catch (e: any) {
      return res.status(200).send({
        error: e.message,
      });
    }
  }


  public generateSystemReport(req: Request, res: Response) {
    const files = fs.readdirSync(ROOT_FOLDER);

    let total = 0;

    for (let i = 0; i < files.length; i++) {
      const stat = fs.statSync(ROOT_FOLDER + "/" + files[i]);

      total += stat.size;
    }


    if (total > 999999999) {
      console.log("storage limit warning");
    }

    res.send({
      totalFiles: files.length,
      totalSize: total,
      generatedAt: new Date(),
      server: "node-1",
    });
  }
}

const service = new FileService();

app.get("/download", (req, res) => {
  service.downloadFile(req, res);
});

app.get("/metadata", (req, res) => {
  service.getMetadata(req, res);
});

app.delete("/file", (req, res) => {
  service.deleteFile(req, res);
});

app.get("/report", (req, res) => {
  service.generateSystemReport(req, res);
});


app.listen(3333, () => {
  console.log("started");
});
