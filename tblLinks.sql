SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

CREATE TABLE `tblCategory` (
  `categoryID` int(10) NOT NULL AUTO_INCREMENT,
  `categoryName` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`categoryID`)
)ENGINE=MyISAM DEFAULT CHARSET=latin1;

INSERT INTO `tblCategory` (`categoryID`, `categoryName`) VALUES
(1, 'Technology'),
(2, 'Accounts'),
(3, 'Shopping'),
(4, 'Entertainment');


CREATE TABLE `tblLinks` (
    `linkID` int(10) NOT NULL AUTO_INCREMENT,
    `categoryID` int(10) NOT NULL,
    `link` varchar (100) NOT NULL,
    `name` varchar (25) NOT NULL,
    PRIMARY KEY (`linkID`),
    KEY `categoryID` (`categoryID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

INSERT INTO `tblLinks` (`categoryID`, `link`, `name`) VALUES 
  (1, 'https://validator.w3.org/', 'HTML Validator');

INSERT INTO `tblLinks` (`categoryID`, `link`, `name`) VALUES 
  (1, 'https://bootswatch.com/', 'Boots watch HomePage');

INSERT INTO `tblLinks` (`categoryID`, `link`, `name`) VALUES 
  (1, 'https://gist.github.com/hofmannsven/9164408', 'Git Cheat Sheet');

INSERT INTO `tblLinks` (`categoryID`, `link`, `name`) VALUES 
  (2, 'https://github.com/chrisbmac/ConcertJavaCalculator', 'My GitHub');

INSERT INTO `tblLinks` (`categoryID`, `link`, `name`) VALUES 
  (2, 'https://nscconline.desire2learn.com/d2l/home', 'NSCC Login');

INSERT INTO `tblLinks` (`categoryID`, `link`, `name`) VALUES 
  (3, 'https://www.chegg.com/shoppingcart?checkoutFlowType=MIXED&added=5587,0efbbbf9-e36a-3a82-bd56-1ca748d0936a,1,NETSUITE,9780357108482|1040,b6705aee-9699-3fca-835a-c3287192387b,1,NETSUITE', 'Chegg');

INSERT INTO `tblLinks` (`categoryID`, `link`, `name`) VALUES 
  (4, 'https://www.youtube.com/', 'youtube');

ALTER TABLE `tblLinks`
  ADD CONSTRAINT `tblLinks` FOREIGN KEY (`categoryID`) REFERENCES `tblCategory` (`categoryID`) ON DELETE CASCADE ON UPDATE CASCADE;


CREATE TABLE `tblLinksLogin` (
    `username` VARCHAR(45) NOT NULL,
    `password` VARCHAR(200) NOT NULL,
    `salt` VARCHAR(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `tblLinksLogin` (`username`, `password`, `salt`) VALUES
('user', 'k7KpFXtX8nIpmupKTdXQgfTSirtMpCMr9oqVJk8UohY=', 'lVc5UCSaJ3wiMHmCUpCnrw==');

ALTER TABLE `tblLinksLogin`
  ADD PRIMARY KEY (`username`);

CREATE TABLE `tblPinnedLinks` (
    `pinnedLinkID`int(10) NOT NULL AUTO_INCREMENT,
    `linkID` int(10) NOT NULL,
    `categoryID` int(10) NOT NULL,
    `link` varchar (100) NOT NULL,
    `name` varchar (25) NOT NULL,
    PRIMARY KEY (`pinnedLinkID`),
    KEY `linkID` (`linkID`),
    KEY `categoryID` (`categoryID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

INSERT INTO `tblPinnedLinks` (`linkID`, `categoryID`, `link`, `name`) VALUES
  (7, 4, 'https://www.youtube.com/', 'youtube');

ALTER TABLE `tblPinnedLinks`
  ADD CONSTRAINT `tblPinnedLinks` FOREIGN KEY (`linkID`) REFERENCES `tblLinks` (`linkID`) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE `tblPinnedLinks`
  ADD CONSTRAINT `tblPinnedLinks` FOREIGN KEY (`categoryID`) REFERENCES `tblCategory` (`categoryID`) ON DELETE CASCADE ON UPDATE CASCADE;