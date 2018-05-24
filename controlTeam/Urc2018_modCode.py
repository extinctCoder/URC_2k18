import os
import glob
import spidev
from time import sleep
import Adafruit_DHT as dht
import paho.mqtt.client as mqtt

os.system('modprobe w1-gpio')
os.system('modprobe w1-therm')

base_dir = '/sys/bus/w1/devices/'
device_folder = glob.glob(base_dir + '28*')[0]
device_file = device_folder + '/w1_slave'

spi =spidev.SpiDev()
spi.open(0,0)

global moisture_sensor_pin
global gas_sensor_pin
global uv_sensor_pin

gas_sensor_pin = 0
soil_sensor_pin = 2
uv_sensor_pin = 4

broker_address = "192.168.0.105"
soil_sensor = "sensor/soil"
uv_sensor = "sensor/uv"
gas_sensor = "sensor/gas"
temp_sensor = "sensor/temp"
air_temp_sensor = "sensor/air/temp"
air_hum_sensor =  "sensor/air/hum"

global soil_sensor_data
global uv_sensor_data
global gas_sensor_data
global temp_sensor_data
global air_temp_sensor_data
global air_hum_sensor_data

def on_publish(client,userdata,result):
    print "Client: ", client
    print "Userdata: : ", userdata
    print "Result: ", result
    pass
def on_log(client, userdata, level, buf):
    print "Log: ",buf
    pass
def on_connect(client, userdata, flags, rc):
    if rc == 0:
        print "Mqtt client connected with broker"
        print "Preparing to send data"
    else:
        print "Unable to connect with broker. Error code: ", rc
    pass

def getGasSensorData():
    rawData = spi.xfer([1, (8 + gas_sensor_pin) << 4, 0])
    processedData = ((rawData[1] & 3) << 8) + rawData[2]
    return processedData

def getSoilSensorData():
    rawData = spi.xfer([1, (8 + soil_sensor_pin) << 4, 0])
    processedData = ((rawData[1] & 3) << 8) + rawData[2]
    return processedData

def getUvSensorData():
    rawData = spi.xfer([1,(8 + uv_sensor_pin) << 4, 0])
    processedData = ((rawData[1] & 3) << 8) + rawData[2]
    if processedData in range(0, 9):
        return 0
    elif processedData in range(10, 46):
        return 1
    elif processedData in range(47, 65):
        return 2
    elif processedData in range(66, 83):
        return 3
    elif processedData in range(84, 103):
        return 4
    elif processedData in range(104, 124):
        return 5
    elif processedData in range(125, 142):
        return 6
    elif processedData in range(143, 162):
        return 7
    elif processedData in range(163, 180):
        return 8
    elif processedData in range(181, 200):
        return 9
    elif processedData in range(201, 221):
        return 10
    elif processedData in range(222, 240):
        return 11
    elif processedData > 240:
        return 12
    else:
        return 0

def readRawTempData():
    file = open(device_file, 'read')
    lines = file.readlines()
    file.close()
    return lines

def getTempSensorData():
    lines = readRawTempData()
    while lines[0].strip()[-3:] != 'YES':
        sleep(0.2)
        lines = readRawTempData()
    value_pos = lines[1].find('t=')
    if value_pos  != -1:
        processedData = lines[1][value_pos + 2:]
    return float(processedData) / 1000.0

def getAirSensorData():
    raw_air_temp, raw_air_hum = dht.read_retry(dht.DHT22, 4)
    try:
        air_temp = '{0:.2f}'.format(raw_air_temp)
        air_hum = '{0:.2f}'.format(raw_air_hum)
    except:
        air_temp = 0
        air_hum = 0
    else:
        pass
    finally:
        sleep(3)
        return air_temp, air_hum


#main_app
print "Wellcome to mqtt broker script"
print "Creating new instance of mqtt client"
client = mqtt.Client()
client.on_connect = on_connect
client.on_publish = on_publish
client.on_log = on_log
print "Connecting to mqtt broker publisher script"
client.connect(broker_address)
client.loop_start()
while 1:
    soil_sensor_data = getSoilSensorData()
    uv_sensor_data = getUvSensorData()
    gas_sensor_data = getGasSensorData()
    temp_sensor_data = getTempSensorData()
    air_temp_sensor_data, air_hum_sensor_data =  getAirSensorData()
    print "publishing data"
    print "soil_sensor_data: ", soil_sensor_data
    print "uv_sensor_data: ", uv_sensor_data
    print "gas_sensor_data: ", gas_sensor_data
    print "temp_sensor_data: ", temp_sensor_data
    print "air_temp_sensor_data: ", air_temp_sensor_data
    print "air_hum_sensor_data: ", air_hum_sensor_data
    client.publish(soil_sensor, soil_sensor_data)
    client.publish(uv_sensor, uv_sensor_data)
    client.publish(gas_sensor, gas_sensor_data)
    client.publish(temp_sensor, temp_sensor_data)
    client.publish(air_temp_sensor, air_temp_sensor_data)
    client.publish(air_hum_sensor, air_hum_sensor_data)

client.loop_stop()
client.disconnect()
