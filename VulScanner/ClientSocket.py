#coding:utf-8
__author__ = 'pylotus'

from socket import *
import time

s = socket(AF_INET,SOCK_STREAM)
s.connect(("localhost",8888))
tm = s.send("Hello world")
s.close()
print type(tm)
