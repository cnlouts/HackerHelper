#-*- encoding:utf-8 -*-
import requests
import os 
import ConfigParser
from lib.utils.log import Logger

class Http(object):
    """description of class"""

    def __init__(self):
        pass
    
    def initCookie(self):
        '''
        :��ʼ�������ļ��е�cookie
        '''
        cookies = {}
        try:
            cfg = ConfigParser.ConfigParser()
            cfg.read(os.path.join(os.getcwd(),'config','setup.ini'))
            cookie = cfg.get('Cookie','cookie')
            _ = cookie.split(';')
            for item in _:
                temp = item.split('=',1)
                key = temp[0]
                value = temp[1]
                cookies[key] = value
        except Exception as e:
            Logger.Error(e)
        return cookies

    def initHeaders(self):
        '''
        :��ʼ�������ļ��е�headers �������
        '''
        headers = {}
        try:
            cfg = ConfigParser.ConfigParser()
            cfg.read(os.path.join(os.getcwd(),'config','setup.ini'))
            useragent = cfg.get('Headers','useragent')
            _ = useragent.split(':')
            headers[_[0]] =useragent[len(_[0])+1:]
        except Exception as e:
            Logger.Error(e)
        return headers

    def get(self,url,**kwargs):
        '''
        :param url: url
        :param kwargs:
        :return: 如果没有使用 cookie  headers 默认使用配置文件的 cookie headers
        '''
        kwargs.setdefault('cookies',self.initCookie())
        kwargs.setdefault('headers',self.initHeaders())
        return requests.get(url,**kwargs)

    def head(self,url,**kwargs):
        kwargs.setdefault('cookies',self.initCookie())
        kwargs.setdefault('headers',self.initHeaders())
        return requests.head(url,**kwargs)

    def post(self,url,data=None,**kwargs):
        '''
        :param url:
        :param data:
        :param kwargs:
        :return: 如果没有使用 cookie  headers 默认使用配置文件的 cookie headers
        '''
        '''
        if 'cookies' not in kwargs and 'headers' not in kwargs:
            cookies = self.initCookie()
            headers = self.initHeaders()
            return requests.post(url,data,cookies = cookies,headers =headers)
        else:
            return requests.post(url,data,kwargs)
        '''
        kwargs.setdefault('cookies',self.initCookie())
        kwargs.setdefault('headers',self.initHeaders())
        return requests.get(url,data=None,**kwargs)


