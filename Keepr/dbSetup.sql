CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- NOTE Profile
CREATE TABLE IF NOT EXISTS profile(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  name VARCHAR(255) NOT NULL,
  picture TEXT NOT NULL,
  email VARCHAR(255) NOT NULL,
  accountId VARCHAR(255) NOT NULL,
  FOREIGN KEY (accountId) REFERENCES accounts(id)
)default charset utf8 COMMENT '';

-- NOTE keeps
CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img TEXT NOT NULL,
  views INT DEFAULT 0,
  kept INT DEFAULT 0,
  vaultKeepId INT,
  FOREIGN KEY (creatorId) REFERENCES accounts (id)
)default charset utf8 COMMENT '';

-- NOTE vaults
CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  isPrivate BOOLEAN DEFAULT false,
  FOREIGN KEY (creatorId) REFERENCES accounts (id)
)default charset utf8 COMMENT '';

-- NOTE vaultKeep
CREATE TABLE IF NOT EXISTS vaultKeep(
  id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
)default charset utf8 COMMENT '';