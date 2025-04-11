CREATE DATABASE  IF NOT EXISTS `db02` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db02`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: db02
-- ------------------------------------------------------
-- Server version	8.0.37

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `materialtype`
--

DROP TABLE IF EXISTS `materialtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `materialtype` (
  `materialtypeID` int NOT NULL AUTO_INCREMENT,
  `materialtypeTitle` varchar(50) NOT NULL,
  `materialtypePercent` double NOT NULL,
  PRIMARY KEY (`materialtypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `materialtype`
--

LOCK TABLES `materialtype` WRITE;
/*!40000 ALTER TABLE `materialtype` DISABLE KEYS */;
INSERT INTO `materialtype` VALUES (1,'Тип материала 1',0.001),(2,'Тип материала 2',0.0095),(3,'Тип материала 3',0.0028),(4,'Тип материала 4',0.0055),(5,'Тип материала 5',0.0034);
/*!40000 ALTER TABLE `materialtype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `materialtypeproduct`
--

DROP TABLE IF EXISTS `materialtypeproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `materialtypeproduct` (
  `materialtypeproductMaterialTypeID` int NOT NULL,
  `materialtypeproductProductArticle` varchar(7) NOT NULL,
  PRIMARY KEY (`materialtypeproductMaterialTypeID`,`materialtypeproductProductArticle`),
  KEY `fk_productArticle_idx` (`materialtypeproductProductArticle`),
  CONSTRAINT `fk_materialtypeID` FOREIGN KEY (`materialtypeproductMaterialTypeID`) REFERENCES `materialtype` (`materialtypeID`) ON UPDATE CASCADE,
  CONSTRAINT `fk_productArticle` FOREIGN KEY (`materialtypeproductProductArticle`) REFERENCES `product` (`productArticle`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `materialtypeproduct`
--

LOCK TABLES `materialtypeproduct` WRITE;
/*!40000 ALTER TABLE `materialtypeproduct` DISABLE KEYS */;
/*!40000 ALTER TABLE `materialtypeproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partner`
--

DROP TABLE IF EXISTS `partner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `partner` (
  `partnerID` int NOT NULL AUTO_INCREMENT,
  `partnerType` int NOT NULL,
  `partnerTitle` varchar(50) NOT NULL,
  `partnerDirector` varchar(50) NOT NULL,
  `partnerEmail` varchar(50) NOT NULL,
  `partnerPhone` varchar(15) NOT NULL,
  `partnerAddress` varchar(100) NOT NULL,
  `partnerINN` varchar(10) DEFAULT NULL,
  `partnerRating` tinyint NOT NULL,
  `partnerDeleted` enum('0','1') NOT NULL,
  PRIMARY KEY (`partnerID`),
  KEY `fk_partnertype_idx` (`partnerType`),
  CONSTRAINT `fk_partnertype` FOREIGN KEY (`partnerType`) REFERENCES `partnertype` (`partnertypeID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partner`
--

LOCK TABLES `partner` WRITE;
/*!40000 ALTER TABLE `partner` DISABLE KEYS */;
INSERT INTO `partner` VALUES (1,1,'База Строитель','Иванова Александра Ивановна','aleksandraivanova@ml.ru','493 123 45 67','652050, Кемеровская область, город Юрга, ул. Лесная, 15','2222455179',7,'0'),(2,2,'Паркет 29','Петров Василий Петрович','vppetrov@vl.ru','987 123 56 78','164500, Архангельская область, город Северодвинск, ул. Строителей, 18','3333888520',7,'0'),(3,3,'Стройсервис','Соловьев Андрей Николаевич','ansolovev@st.ru','812 223 32 00','188910, Ленинградская область, город Приморск, ул. Парковая, 21','4440391035',7,'0'),(4,4,'Ремонт и отделка','Воробьева Екатерина Валерьевна','ekaterina.vorobeva@ml.ru','444 222 33 11','143960, Московская область, город Реутов, ул. Свободы, 51','1111520857',5,'0'),(5,1,'МонтажПро','Степанов Степан Сергеевич','stepanov@stepan.ru','912 888 33 33','309500, Белгородская область, город Старый Оскол, ул. Рабочая, 122','5552431140',10,'0');
/*!40000 ALTER TABLE `partner` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partnerproduct`
--

DROP TABLE IF EXISTS `partnerproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `partnerproduct` (
  `partnerproductProductArticle` varchar(7) NOT NULL,
  `partnerproductPartnerID` int NOT NULL,
  `partnerproductCount` int NOT NULL,
  `partnerproductDate` date NOT NULL,
  PRIMARY KEY (`partnerproductProductArticle`,`partnerproductPartnerID`),
  KEY `fk_partner_idx` (`partnerproductPartnerID`),
  CONSTRAINT `fk_partner` FOREIGN KEY (`partnerproductPartnerID`) REFERENCES `partner` (`partnerID`) ON UPDATE CASCADE,
  CONSTRAINT `fk_product` FOREIGN KEY (`partnerproductProductArticle`) REFERENCES `product` (`productArticle`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partnerproduct`
--

LOCK TABLES `partnerproduct` WRITE;
/*!40000 ALTER TABLE `partnerproduct` DISABLE KEYS */;
INSERT INTO `partnerproduct` VALUES ('5012543',2,1250,'2023-05-17'),('5012543',4,4500,'2024-05-14'),('7028748',1,37400,'2024-06-07'),('7028748',4,59050,'2023-03-20'),('7028748',5,670000,'2023-11-10'),('7750282',1,12350,'2023-12-18'),('7750282',2,1000,'2024-06-07'),('7750282',4,37200,'2024-03-12'),('7750282',5,50000,'2023-09-19'),('8758385',1,15500,'2023-03-23'),('8758385',2,7550,'2024-07-01'),('8758385',3,7250,'2023-01-22'),('8758385',5,35000,'2024-04-15'),('8858958',2,35000,'2022-12-02'),('8858958',3,2500,'2024-07-05'),('8858958',5,25000,'2024-06-12');
/*!40000 ALTER TABLE `partnerproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partnertype`
--

DROP TABLE IF EXISTS `partnertype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `partnertype` (
  `partnertypeID` int NOT NULL AUTO_INCREMENT,
  `partnertypeTitle` varchar(50) NOT NULL,
  PRIMARY KEY (`partnertypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partnertype`
--

LOCK TABLES `partnertype` WRITE;
/*!40000 ALTER TABLE `partnertype` DISABLE KEYS */;
INSERT INTO `partnertype` VALUES (1,'ЗАО'),(2,'ООО'),(3,'ПАО'),(4,'ОАО');
/*!40000 ALTER TABLE `partnertype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `productArticle` varchar(7) NOT NULL,
  `productType` int NOT NULL,
  `productTitle` varchar(100) NOT NULL,
  `productMinCost` double NOT NULL,
  PRIMARY KEY (`productArticle`),
  KEY `fk_producttype_idx` (`productType`),
  CONSTRAINT `fk_producttype` FOREIGN KEY (`productType`) REFERENCES `producttype` (`producttypeID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES ('5012543',4,'Пробковое напольное клеевое покрытие 32 класс 4 мм',5450.59),('7028748',1,'Ламинат Дуб серый 32 класс 8 мм с фаской',3890.41),('7750282',1,'Ламинат Дуб дымчато-белый 33 класс 12 мм',1799.33),('8758385',3,'Паркетная доска Ясень темный однополосная 14 мм',4456.9),('8858958',3,'Инженерная доска Дуб Французская елка однополосная 12 мм',7330.99);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `producttype`
--

DROP TABLE IF EXISTS `producttype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `producttype` (
  `producttypeID` int NOT NULL AUTO_INCREMENT,
  `producttypeTitle` varchar(50) NOT NULL,
  `producttypeCoefficient` double NOT NULL,
  PRIMARY KEY (`producttypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `producttype`
--

LOCK TABLES `producttype` WRITE;
/*!40000 ALTER TABLE `producttype` DISABLE KEYS */;
INSERT INTO `producttype` VALUES (1,'Ламинат',2.35),(2,'Массивная доска',5.15),(3,'Паркетная доска',4.34),(4,'Пробковое покрытие',1.5);
/*!40000 ALTER TABLE `producttype` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-11 14:05:37
