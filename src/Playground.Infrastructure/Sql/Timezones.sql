CREATE TABLE "Timezones" (
    "Abbreviation" VARCHAR(10) PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL
);

INSERT INTO "Timezones" ("Abbreviation", "Name") VALUES
('ICT', 'SE Asia Standard Time'),
('GMT', 'GMT Standard Time'),
('PDT', 'Pacific Daylight Time'),
('EST', 'Eastern Standard Time'),
('CST', 'Central Standard Time'),
('MST', 'Mountain Standard Time'),
('PST', 'Pacific Standard Time'),
('CET', 'Central European Time'),
('EET', 'Eastern European Time'),
('JST', 'Tokyo Standard Time'),
('AEST', 'AUS Eastern Standard Time'),
('ACST', 'Cen. Australia Standard Time'),
('AWST', 'W. Australia Standard Time');