# SQL interview task - remove duplicates

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


# SQL interview task -  Enable temporal table support for users

**Description**
Implement temporal table support for the users table so every insert, update, and delete is recorded historically. The system must support querying the state of the users table as of a specific date/time.

**Goal**
We need full audit/history support for user records. Any change to a user row should be preserved so we can answer questions like:

```
SELECT *
FROM users_as_of('2026-05-01 12:00:00');
```

or:
```
SELECT *
FROM users_history
WHERE user_id = '...'
ORDER BY valid_from;
```

**Requirements**

Add a history table, for example:
users_history

**It should store:**

- original users row data
- operation type: INSERT, UPDATE, DELETE
- valid_from
- valid_to
- changed timestamp
- changed by user/service, if available
- transaction/request ID, if available

**Acceptance criteria**

- All changes to users are recorded in users_history.
- We can query user state as of any timestamp.
- Updates preserve previous values.
- Deletes are auditable.
- Temporal logic works for multiple updates to the same user.
- Migration can be applied and rolled back safely.
- Query performance is acceptable for production-sized data.



# SQL interview task -  Dynamic formula calculation engine

You have a table:
```
CREATE TABLE metrics (
  id BIGSERIAL PRIMARY KEY,
  patient_id UUID NOT NULL,
  systolic NUMERIC,
  diastolic NUMERIC,
  weight_kg NUMERIC,
  height_cm NUMERIC,
  age NUMERIC
);
```

```
CREATE TABLE formulas (
  id BIGSERIAL PRIMARY KEY,
  code TEXT NOT NULL UNIQUE,
  expression TEXT NOT NULL
);
```

Example formulas:
```
bmi = weight_kg / ((height_cm / 100) ^ 2)
pulse_pressure = systolic - diastolic
risk_score = age * 0.2 + systolic * 0.1 + bmi * 0.3
```
**Question:**

Design a PostgreSQL-based solution that calculates formulas dynamically for each row.
