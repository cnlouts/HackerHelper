#-*- encoding:utf-8 -*-
import pymssql
from lib.utils.log import Logger

class mssql(object):
    """description of class"""

    def __init__(self,host,user,pwd,db="tempdb",port="1433"):
        self.host = host
        self.user = user
        self.pwd = pwd
        self.db = db
        self.port = port

    def __getConnect(self):
        cursor =None
        try:
            self.conn = pymssql.connect(host=self.host,user=self.user,password=self.pwd,\
                                        database=self.db,charset="utf8",port=self.port)
            cursor = self.conn.cursor()
        except Exception as e:
            Logger.Error(e)
        return cursor

    def execQuery(self,sqlcmd):
        resList = None
        cursor = self.__getConnect()
        if  cursor:
            cursor.execute(sqlcmd)
            resList = cursor.fetchall()

            self.conn.close()
        return resList

    def execNoneQuery(self,sqlcmd):
        cursor = self.__getConnect()
        cursor.execute(sqlcmd)
        self.conn.commit()
        self.conn.close()




