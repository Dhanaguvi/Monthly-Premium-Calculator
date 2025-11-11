## Assumptions & Clarifications:

Simple explaination aabout the attached application is calculating the monthly insurance based on the provided data's and the formula.

Here the inputs are taken through a form. from the .net core application API the components are getting the inputs and processed it then show's it. I have performed the basic validations also for the added controls.

Also the minimum amount of styling given for the added controls.


## 🧩 Database Design

### 1. OccupationRating

| Column Name         | Data Type     | Constraints / Description |
|----------------------|---------------|----------------------------|
| **OccupationRatingId** | INT | Primary Key |
| **RatingName** | VARCHAR(50) | NOT NULL, e.g. *Professional*, *Light Manual* |
| **Factor** | DECIMAL(8,4) | NOT NULL, e.g. *1.5000* (used in premium formula) |

---

### 2. Occupation

| Column Name         | Data Type     | Constraints / Description |
|----------------------|---------------|----------------------------|
| **OccupationId** | INT | Primary Key |
| **OccupationName** | VARCHAR(100) | NOT NULL, e.g. *Doctor*, *Cleaner* |
| **OccupationRatingId** | INT | Foreign Key → `OccupationRating.OccupationRatingId`, NOT NULL |

---

### 3. Member

| Column Name         | Data Type     | Constraints / Description |
|----------------------|---------------|----------------------------|
| **MemberId** | BIGINT | Primary Key |
| **FullName** | VARCHAR(200) | NOT NULL, e.g. *John Doe* |
| **DateOfBirth** | DATE | NOT NULL, stored as `YYYY-MM-01` if only `MM/YYYY` provided |
| **AgeNextBirthday** | INT | NOT NULL |
| **UsualOccupationId** | INT | Foreign Key → `Occupation.OccupationId`, NOT NULL |

---

### 4. PremiumCalculation

| Column Name         | Data Type     | Constraints / Description |
|----------------------|---------------|----------------------------|
| **PremiumCalculationId** | BIGINT | Primary Key |
| **MemberId** | BIGINT | Foreign Key → `Member.MemberId`, NOT NULL |
| **OccupationId** | INT | Foreign Key → `Occupation.OccupationId`, NOT NULL |
| **AgeNextBirthday** | INT | NOT NULL |
| **DateOfBirth** | DATE | NOT NULL |
| **DeathSumInsured** | DECIMAL(18,2) | NOT NULL |
| **OccupationFactor** | DECIMAL(8,4) | NOT NULL, snapshot of factor used in formula |
| **CalculatedMonthlyPremium** | DECIMAL(18,2) | NOT NULL, result of formula calculation |
| **CalculatedAt** | DATETIME | NOT NULL, default = current timestamp |

---

### 🧮 Formula Used

\[
\text{Monthly Premium} = \frac{\text{Death Sum Insured} \times \text{Occupation Factor} \times \text{Age}}{1000 \times 12}
\]
