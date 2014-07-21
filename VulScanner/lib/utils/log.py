#-*- encoding:utf-8 -*-
import logging
import os
import ConfigParser
import datetime
import sys
import traceback

class Log(object):
    
    def __init__(self, logger_name='vulScanLogger'):
        self.init_logger(logger_name)

    def init_logger(self,logger_name):
        '''
        :初始化日志记录器
        '''
        self.logger = logging.getLogger(logger_name)
        formatter = logging.Formatter("\r[%(asctime)s] [%(levelname)s] %(message)s", "%Y-%m-%d %H:%M:%S")
        log_path = os.path.join(os.getcwd(),self.get_log_path(),str(datetime.date.today())+'.log')
        file_handler = logging.FileHandler(log_path)
        file_handler.setFormatter(formatter)
        self.logger.addHandler(file_handler)
        self.logger.setLevel(logging.DEBUG)

    def get_log_path(self):
        '''
        :return 返回配置文件中日志存放的路径
        '''
        #E:\VSProject\HackerHelper\VulScanner
        work_dir = os.getcwd()
        config_path = os.path.join(work_dir,'config','setup.ini')
        cfg = ConfigParser.ConfigParser()
        cfg.read(config_path)
        log_file_path = cfg.get('LogFile','path')
        return log_file_path

    def Info(self,msg):
        self.logger.info(msg)

    def Debug(self,msg):
        self.logger.debug(msg)

    def Error(self,msg):
        if sys.exc_info():
            msg = msg + ''.join(traceback.format_exc())
        self.logger.error(msg)
'''
:类似单利的方法
'''
Logger = Log()
