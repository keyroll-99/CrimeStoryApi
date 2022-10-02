CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Hash" UUID UNIQUE NOT NULL,
    "Username" VARCHAR(80) UNIQUE NOT NULL,
    "Password" VARCHAR(80) NOT NULL,
    "CreateAt" TIMESTAMP NOT NULL default CURRENT_TIMESTAMP,
    "UpdateAt" TIMESTAMP null default NULL
);