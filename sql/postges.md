# SQL interview task

You have a PostgreSQL table with **~10M** rows:
```
CREATE TABLE events (
  id BIGSERIAL PRIMARY KEY,
  event_date DATE NOT NULL,
  event_data JSONB NOT NULL,
  created_at TIMESTAMP NOT NULL DEFAULT now()
);
```
Duplicate rows are defined as rows with the same:

`(event_date, event_data)`

Write SQL to display duplicate groups and the number of extra duplicate rows that could be removed.

Expected output:
```
event_date | event_data | total_rows | duplicate_rows_to_remove
-----------+------------+------------+--------------------------
2026-01-01 | {...}      | 5          | 4
2026-01-02 | {...}      | 2          | 1
```
