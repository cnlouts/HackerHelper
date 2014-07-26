/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50045
Source Host           : 127.0.0.1:3306
Source Database       : topscan

Target Server Type    : MYSQL
Target Server Version : 50045
File Encoding         : 65001

Date: 2014-06-11 18:57:51
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for rule
-- ----------------------------
DROP TABLE IF EXISTS `rule`;
CREATE TABLE `rule` (
  `id` int(11) NOT NULL auto_increment,
  `rule_id` int(11) NOT NULL,
  `rule_name` char(255) NOT NULL,
  `run_type` tinyint(2) NOT NULL,
  `priority` tinyint(2) NOT NULL,
  `filename` char(255) NOT NULL,
  `category_id` int(11) NOT NULL,
  `description` mediumtext NOT NULL,
  `solution` mediumtext NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rule
-- ----------------------------
INSERT INTO `rule` VALUES ('1', '1', '', 'robots.txt站点结构泄露', '2', '1', 'robots_leak', '1', '', '');
INSERT INTO `rule` VALUES ('2', '2', '', 'Web应用指纹识别', '2', '1', 'app_fingure', '1', '', '');
INSERT INTO `rule` VALUES ('3', '3', '', '内部IP地址泄露', '1', '10', 'inter_ip_leak', '1', '', '');
INSERT INTO `rule` VALUES ('7', '7', '', 'SQL注入', '1', '1', 'sql_inject', '2', '', '');
INSERT INTO `rule` VALUES ('4', '4', '', '服务器错误', '1', '1', 'server_error', '1', '', '');
INSERT INTO `rule` VALUES ('5', '5', '', '发现死链接', '1', '9', 'deak_link', '1', '', '');
INSERT INTO `rule` VALUES ('6', '6', '', '发现后台登陆页面', '2', '1', 'adminpage_leak', '1', '', '');
INSERT INTO `rule` VALUES ('8', '8', '', '跨站脚本攻击', '1', '1', 'xss', '0', '', '');
