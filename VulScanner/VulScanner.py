#-*- coding:utf-8 -*-
import ConfigParser
import os

from lib.core.net.http import Http
from lib.utils.encryptHelper import encryptHelp

from lib.core.net.mssql import mssql
from lib.utils.log import Logger

#Logger.Info("test inf sdasdasdaso")
#print encryptHelp.md5encode('123456')
#print encryptHelp.sha1encode('123456')
#print encryptHelp.sha224encode('123456')
#print encryptHelp.sha256encode('123456')
#print encryptHelp.sha384encode('123456')
#print encryptHelp.sha512encode('123456')

#print encryptHelp.base64encode('123456')
#print encryptHelp.base64decode(encryptHelp.base64encode('123456'))

def test_encryptHelp():
    print encryptHelp.asciiC2I('abcde')

def test_get_cookie():
    cfg = ConfigParser.ConfigParser()
    cfg.read(os.path.join(os.getcwd(),'config','setup.ini'))
    print cfg.get('Headers','useragent')

def test_header():
    test_http = Http()
    #print test_http.initHeaders()
    r = test_http.get('http://www.baidu.com')
    print r.status_code
    #print r.text

if __name__ == "__main__":
    '''
    test = Http()
    print test.initCookie()
    print test.initHeaders()
    #test.get('http://www.baidu.com')
    r = test.get('http://www.baidu.com')
    print r.cookies
    print r.status_code
    print r.headers
    '''
    '''
    from lib.core.net.mssql import mssql
    test_mssql = mssql("127.0.0.1","sa","123456","master")
    print   test_mssql.getConnect()
    '''
    mssql = mssql("127.0.0.1","sa","123456")
    resList = mssql.execQuery("select @@VERSION")
    for item in resList:
        print item

