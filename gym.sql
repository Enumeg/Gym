-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.1.51-community - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL Version:             8.3.0.4694
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping database structure for gym
DROP DATABASE IF EXISTS `gym`;
CREATE DATABASE IF NOT EXISTS `gym` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `gym`;


-- Dumping structure for table gym.customers
DROP TABLE IF EXISTS `customers`;
CREATE TABLE IF NOT EXISTS `customers` (
  `cus_id` int(100) NOT NULL AUTO_INCREMENT,
  `cus_per_id` int(100) NOT NULL,
  `cus_join_date` date DEFAULT NULL,
  `cus_height` varchar(15) DEFAULT NULL,
  `cus_weight` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`cus_id`),
  KEY `FK_customers_persons` (`cus_per_id`),
  CONSTRAINT `FK_customers_persons` FOREIGN KEY (`cus_per_id`) REFERENCES `persons` (`per_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.customers: ~2 rows (approximately)
DELETE FROM `customers`;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` (`cus_id`, `cus_per_id`, `cus_join_date`, `cus_height`, `cus_weight`) VALUES
	(1, 30, NULL, '170', '80'),
	(2, 31, NULL, '11', '11');
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;


-- Dumping structure for table gym.customer_services
DROP TABLE IF EXISTS `customer_services`;
CREATE TABLE IF NOT EXISTS `customer_services` (
  `cs_id` int(240) NOT NULL AUTO_INCREMENT,
  `cs_cus_id` int(100) NOT NULL,
  `cs_date` date NOT NULL,
  `cs_ser_id` int(10) NOT NULL,
  `cs_value` decimal(10,2) NOT NULL,
  `cs_paid` decimal(10,2) NOT NULL,
  PRIMARY KEY (`cs_id`),
  KEY `FK_customer_services_customers` (`cs_cus_id`),
  KEY `FK_customer_services_service` (`cs_ser_id`),
  CONSTRAINT `FK_customer_services_customers` FOREIGN KEY (`cs_cus_id`) REFERENCES `customers` (`cus_id`) ON DELETE CASCADE,
  CONSTRAINT `FK_customer_services_service` FOREIGN KEY (`cs_ser_id`) REFERENCES `service` (`ser_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table gym.customer_services: ~0 rows (approximately)
DELETE FROM `customer_services`;
/*!40000 ALTER TABLE `customer_services` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer_services` ENABLE KEYS */;


-- Dumping structure for table gym.employees
DROP TABLE IF EXISTS `employees`;
CREATE TABLE IF NOT EXISTS `employees` (
  `emp_id` int(100) NOT NULL AUTO_INCREMENT,
  `emp_per_id` int(100) NOT NULL,
  `emp_job_id` int(10) NOT NULL,
  `emp_salary` decimal(10,2) NOT NULL,
  `emp_join_date` date DEFAULT NULL,
  PRIMARY KEY (`emp_id`),
  KEY `FK_employees_persons` (`emp_per_id`),
  KEY `FK_employees_jobs` (`emp_job_id`),
  CONSTRAINT `FK_employees_jobs` FOREIGN KEY (`emp_job_id`) REFERENCES `jobs` (`job_id`) ON DELETE CASCADE,
  CONSTRAINT `FK_employees_persons` FOREIGN KEY (`emp_per_id`) REFERENCES `persons` (`per_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.employees: ~1 rows (approximately)
DELETE FROM `employees`;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` (`emp_id`, `emp_per_id`, `emp_job_id`, `emp_salary`, `emp_join_date`) VALUES
	(1, 28, 2, 1000.00, '2014-01-01');
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;


-- Dumping structure for table gym.employee_absence
DROP TABLE IF EXISTS `employee_absence`;
CREATE TABLE IF NOT EXISTS `employee_absence` (
  `emc_id` int(100) NOT NULL AUTO_INCREMENT,
  `emc_emp_id` int(100) NOT NULL,
  `emc_date` date NOT NULL,
  `emc_reason` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`emc_id`),
  KEY `FK_employee_absence_employees` (`emc_emp_id`),
  CONSTRAINT `FK_employee_absence_employees` FOREIGN KEY (`emc_emp_id`) REFERENCES `employees` (`emp_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.employee_absence: ~1 rows (approximately)
DELETE FROM `employee_absence`;
/*!40000 ALTER TABLE `employee_absence` DISABLE KEYS */;
INSERT INTO `employee_absence` (`emc_id`, `emc_emp_id`, `emc_date`, `emc_reason`) VALUES
	(1, 1, '2014-01-01', 'jjj');
/*!40000 ALTER TABLE `employee_absence` ENABLE KEYS */;


-- Dumping structure for table gym.employee_tax_bonus
DROP TABLE IF EXISTS `employee_tax_bonus`;
CREATE TABLE IF NOT EXISTS `employee_tax_bonus` (
  `emh_id` int(100) NOT NULL AUTO_INCREMENT,
  `emh_emp_id` int(100) NOT NULL,
  `emh_date` date NOT NULL,
  `emh_nm_id` int(10) NOT NULL,
  `emh_reason` varchar(300) DEFAULT NULL,
  `emh_value` decimal(10,2) NOT NULL,
  PRIMARY KEY (`emh_id`),
  KEY `FK_employee_tax_bonus_employees` (`emh_emp_id`),
  KEY `FK_employee_tax_bonus_names` (`emh_nm_id`),
  CONSTRAINT `FK_employee_tax_bonus_employees` FOREIGN KEY (`emh_emp_id`) REFERENCES `employees` (`emp_id`) ON DELETE CASCADE,
  CONSTRAINT `FK_employee_tax_bonus_names` FOREIGN KEY (`emh_nm_id`) REFERENCES `names` (`nm_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.employee_tax_bonus: ~2 rows (approximately)
DELETE FROM `employee_tax_bonus`;
/*!40000 ALTER TABLE `employee_tax_bonus` DISABLE KEYS */;
INSERT INTO `employee_tax_bonus` (`emh_id`, `emh_emp_id`, `emh_date`, `emh_nm_id`, `emh_reason`, `emh_value`) VALUES
	(1, 1, '2014-01-01', 1, 'jjj', 123.00),
	(2, 1, '2014-01-01', 3, 'hhh', 678.00);
/*!40000 ALTER TABLE `employee_tax_bonus` ENABLE KEYS */;


-- Dumping structure for table gym.groups
DROP TABLE IF EXISTS `groups`;
CREATE TABLE IF NOT EXISTS `groups` (
  `grp_id` int(10) NOT NULL AUTO_INCREMENT,
  `grp_name` varchar(50) NOT NULL,
  PRIMARY KEY (`grp_id`,`grp_name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.groups: ~2 rows (approximately)
DELETE FROM `groups`;
/*!40000 ALTER TABLE `groups` DISABLE KEYS */;
INSERT INTO `groups` (`grp_id`, `grp_name`) VALUES
	(1, 'Admin-مدير'),
	(2, 'reception-إستقبال');
/*!40000 ALTER TABLE `groups` ENABLE KEYS */;


-- Dumping structure for table gym.height_weight
DROP TABLE IF EXISTS `height_weight`;
CREATE TABLE IF NOT EXISTS `height_weight` (
  `hw_id` int(240) NOT NULL AUTO_INCREMENT,
  `hw_cus_id` int(100) NOT NULL,
  `hw_date` date DEFAULT NULL,
  `hw_height` varchar(15) NOT NULL,
  `hw_weight` varchar(15) NOT NULL,
  `hw_img` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`hw_id`),
  KEY `FK_height_weight_customers` (`hw_cus_id`),
  CONSTRAINT `FK_height_weight_customers` FOREIGN KEY (`hw_cus_id`) REFERENCES `customers` (`cus_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.height_weight: ~3 rows (approximately)
DELETE FROM `height_weight`;
/*!40000 ALTER TABLE `height_weight` DISABLE KEYS */;
INSERT INTO `height_weight` (`hw_id`, `hw_cus_id`, `hw_date`, `hw_height`, `hw_weight`, `hw_img`) VALUES
	(1, 1, '2014-01-07', '170', '80', 'D:\\GYM\\1.jpg'),
	(2, 2, '2014-01-07', '11', '11', NULL),
	(3, 1, '2014-01-17', '55', '55', 'D:\\GYM\\3.jpg');
/*!40000 ALTER TABLE `height_weight` ENABLE KEYS */;


-- Dumping structure for table gym.jobs
DROP TABLE IF EXISTS `jobs`;
CREATE TABLE IF NOT EXISTS `jobs` (
  `job_id` int(10) NOT NULL AUTO_INCREMENT,
  `job_name` varchar(100) NOT NULL,
  PRIMARY KEY (`job_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.jobs: ~3 rows (approximately)
DELETE FROM `jobs`;
/*!40000 ALTER TABLE `jobs` DISABLE KEYS */;
INSERT INTO `jobs` (`job_id`, `job_name`) VALUES
	(1, 'مدير'),
	(2, 'إستقبال'),
	(3, 'مدرب');
/*!40000 ALTER TABLE `jobs` ENABLE KEYS */;


-- Dumping structure for table gym.names
DROP TABLE IF EXISTS `names`;
CREATE TABLE IF NOT EXISTS `names` (
  `nm_id` int(10) NOT NULL AUTO_INCREMENT,
  `nm_name` varchar(100) NOT NULL,
  PRIMARY KEY (`nm_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.names: ~4 rows (approximately)
DELETE FROM `names`;
/*!40000 ALTER TABLE `names` DISABLE KEYS */;
INSERT INTO `names` (`nm_id`, `nm_name`) VALUES
	(1, 'خصومات'),
	(2, 'سلف'),
	(3, 'حوافز'),
	(4, 'إضافى');
/*!40000 ALTER TABLE `names` ENABLE KEYS */;


-- Dumping structure for table gym.outcome
DROP TABLE IF EXISTS `outcome`;
CREATE TABLE IF NOT EXISTS `outcome` (
  `out_id` int(250) NOT NULL AUTO_INCREMENT,
  `out_ott_id` int(10) NOT NULL,
  `out_date` date NOT NULL,
  `out_value` decimal(10,2) NOT NULL,
  `out_description` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`out_id`),
  KEY `FK_outcome_outcome_types` (`out_ott_id`),
  CONSTRAINT `FK_outcome_outcome_types` FOREIGN KEY (`out_ott_id`) REFERENCES `outcome_types` (`ott_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.outcome: ~3 rows (approximately)
DELETE FROM `outcome`;
/*!40000 ALTER TABLE `outcome` DISABLE KEYS */;
INSERT INTO `outcome` (`out_id`, `out_ott_id`, `out_date`, `out_value`, `out_description`) VALUES
	(10, 4, '2013-12-31', 15.00, 'rent place'),
	(11, 5, '2014-01-01', 500.00, 'ay 7aga'),
	(12, 4, '2014-01-07', 200.00, 'nhjvnhjgf');
/*!40000 ALTER TABLE `outcome` ENABLE KEYS */;


-- Dumping structure for table gym.outcome_types
DROP TABLE IF EXISTS `outcome_types`;
CREATE TABLE IF NOT EXISTS `outcome_types` (
  `ott_id` int(10) NOT NULL AUTO_INCREMENT,
  `ott_name` varchar(100) NOT NULL,
  PRIMARY KEY (`ott_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='انواع المصروفات';

-- Dumping data for table gym.outcome_types: ~2 rows (approximately)
DELETE FROM `outcome_types`;
/*!40000 ALTER TABLE `outcome_types` DISABLE KEYS */;
INSERT INTO `outcome_types` (`ott_id`, `ott_name`) VALUES
	(4, 'rent'),
	(5, 'gdeda');
/*!40000 ALTER TABLE `outcome_types` ENABLE KEYS */;


-- Dumping structure for table gym.payments
DROP TABLE IF EXISTS `payments`;
CREATE TABLE IF NOT EXISTS `payments` (
  `pay_id` int(200) NOT NULL AUTO_INCREMENT,
  `pay_cus_id` int(100) NOT NULL,
  `pay_date` date NOT NULL,
  `pay_value` decimal(10,2) NOT NULL,
  PRIMARY KEY (`pay_id`),
  KEY `FK_payments_customers` (`pay_cus_id`),
  CONSTRAINT `FK_payments_customers` FOREIGN KEY (`pay_cus_id`) REFERENCES `customers` (`cus_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table gym.payments: ~0 rows (approximately)
DELETE FROM `payments`;
/*!40000 ALTER TABLE `payments` DISABLE KEYS */;
/*!40000 ALTER TABLE `payments` ENABLE KEYS */;


-- Dumping structure for table gym.persons
DROP TABLE IF EXISTS `persons`;
CREATE TABLE IF NOT EXISTS `persons` (
  `per_id` int(100) NOT NULL AUTO_INCREMENT,
  `per_name` varchar(250) NOT NULL,
  `per_address` varchar(200) DEFAULT NULL,
  `per_mail` varchar(100) DEFAULT NULL,
  `per_mobile` varchar(100) DEFAULT NULL,
  `per_age` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`per_id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.persons: ~10 rows (approximately)
DELETE FROM `persons`;
/*!40000 ALTER TABLE `persons` DISABLE KEYS */;
INSERT INTO `persons` (`per_id`, `per_name`, `per_address`, `per_mail`, `per_mobile`, `per_age`) VALUES
	(22, 'first', '18', 'it', '011', '20'),
	(23, 'second', '16 farid', 'id', '011', '18'),
	(24, 'third', '16far', 'idd', '090', '90'),
	(25, 'forth', '8hjh', 'ihbvhn', '01928', '19'),
	(26, 'mohamed atef', '16 farid', 'it', '011', '23'),
	(27, 'amr', '27ghh', 'isgh', '0192', '24'),
	(28, 'employee1', '16 hjhjh', NULL, '0123', NULL),
	(29, 'khaled', '8bjnb', 'ihkjg', '08908', '30'),
	(30, 'hh', 'jh7', 'jh6', 'khj6', '22'),
	(31, 'mm', 'hg', 'jh', 'jh', '22');
/*!40000 ALTER TABLE `persons` ENABLE KEYS */;


-- Dumping structure for table gym.service
DROP TABLE IF EXISTS `service`;
CREATE TABLE IF NOT EXISTS `service` (
  `ser_id` int(10) NOT NULL AUTO_INCREMENT,
  `ser_name` varchar(200) NOT NULL,
  `ser_value` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ser_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.service: ~3 rows (approximately)
DELETE FROM `service`;
/*!40000 ALTER TABLE `service` DISABLE KEYS */;
INSERT INTO `service` (`ser_id`, `ser_name`, `ser_value`) VALUES
	(3, 'gym', 100.00),
	(4, 'gym2', 150.00),
	(5, 'gym3', 200.00);
/*!40000 ALTER TABLE `service` ENABLE KEYS */;


-- Dumping structure for table gym.users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `user_id` int(10) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(30) NOT NULL,
  `user_pass` varchar(20) NOT NULL,
  `user_grp_id` int(1) DEFAULT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_name` (`user_name`),
  KEY `FK_users_group` (`user_grp_id`),
  CONSTRAINT `FK_users_groups` FOREIGN KEY (`user_grp_id`) REFERENCES `groups` (`grp_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table gym.users: ~2 rows (approximately)
DELETE FROM `users`;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`user_id`, `user_name`, `user_pass`, `user_grp_id`) VALUES
	(1, 'm', '-842352701', 1),
	(2, 'k', '-842352699', 2);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
