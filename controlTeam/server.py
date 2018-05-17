# -*- coding: utf-8 -*-
"""
Created on Wed May 09 05:59:00 2018

@author: darthVader
"""

import socket
ip = '127.0.0.1'
port = 8888
BUFFER_SIZE = 10
sensor1 = 1
sensor2 = 2
sensor3 = 3
sensor4 = 4
sensor5 = 5
sensor6 = 6


def feedData(sensor1,sensor2,sensor3,sensor4,sensor5,sensor6):
    data = str(sensor1)+","+str(sensor2)+","+str(sensor3)+","+str(sensor4)+","+str(sensor5)+","+str(sensor6)
    return data

def printMsg(msg):
    print msg
    return

printMsg("welcome to py socket script")
printMsg("trying to open a port")
server = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
printMsg("server created")
try:
    server.bind((ip,port))
except socket.error as ex:
    printMsg(ex[1])
else:
    printMsg("server bind successfull")
    while(1):
        try:
            server.listen(10)
            while(1):
                try:
                    printMsg("waiting for the gui")
                    con, add = server.accept()
                    printMsg(add[1])
                except server.error as ex:
                    printMsg(ex[1])
                else:
                    printMsg("sending data to gui")
                    while(1):
                        sensor1 = sensor1 + 1
                        sensor2 = sensor2 + 1
                        sensor3 = sensor3 + 1
                        sensor4 = sensor4 + 1
                        sensor5 = sensor5 + 1
                        sensor6 = sensor6 + 1
                        data = feedData(sensor1,sensor2,sensor3,sensor4,sensor5,sensor6)
                        printMsg(data)
                        con.send(data)
        except Exception as ex:
            printMsg("gui offline")
            printMsg(ex)
finally:
    server.close()
    printMsg("thank you")