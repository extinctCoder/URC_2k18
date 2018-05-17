import paho.mqtt.client as mqtt
import time
broker_address = "192.168.0.101"

soil_sensor = "sensor/soil"
uv_sensor = "sensor/uv"
gas_sensor = "sensor/gas"
temp_sensor = "sensor/temp"
air_temp_sensor = "sensor/air/temp"
air_hum_sensor =  "sensor/air/hum"

def on_publish(client,userdata,result):
    print("Client: ", client)
    print("Userdata: : ", userdata)
    print("Result: ", result)
    pass
def on_log(client, userdata, level, buf):
    print("Log: ",buf)
    pass
def on_connect(client, userdata, flags, rc):
    if rc == 0:
        print("Mqtt client connected with broker")
        print("Preparing to send data")
    else:
        print("Unable to connect with broker. Error code: ", rc)
    pass

print("Wellcome to mqtt broker script")
print("Creating new instance of mqtt client")
client = mqtt.Client()
client.on_connect = on_connect
client.on_publish = on_publish
client.on_log = on_log
print("Connecting to mqtt broker publisher script")
client.connect(broker_address)
client.loop_start()
while 1:
    client.publish(soil_sensor,12)
    client.publish(uv_sensor,13)
    client.publish(gas_sensor,14)
    client.publish(temp_sensor,15)
    client.publish(air_temp_sensor,16)
    client.publish(air_hum_sensor,17)
    time.sleep(4)

client.loop_stop()
client.disconnect()
