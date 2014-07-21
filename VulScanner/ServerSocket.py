#coding:utf-8
__author__ = 'pylotus'

from socket import *
import time

s = socket(AF_INET, SOCK_STREAM)
s.bind(('',8888))
s.listen(5)

while True:
    client,addr = s.accept()
    print client.recv(1024).decode("ascii")