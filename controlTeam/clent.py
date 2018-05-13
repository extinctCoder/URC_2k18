# -*- coding: utf-8 -*-
"""
Created on Wed May 09 05:59:54 2018

@author: darthVader
"""

import socket

ip = "127.0.0.1"
port = 8888
buffer_size = 1024
def printMsg(msg):
    print msg
    return
printMsg("wellcome to py client script")
client = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
try:
    client.connect((ip,port))
except socket.error as ex:
    printMsg(ex[1])
else:
    printMsg("server found")
    while(1):
        try:
            data = client.recv(buffer_size)
        except socket.error as ex:
            printMsg(ex[1])
        else:
            printMsg(data)
finally:
    printMsg("server offline")
        