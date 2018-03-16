-------------
-- wifi
-------------
print("Setting up WIFI...")
wifi.setmode(wifi.STATION)
station_cfg={}
station_cfg.ssid="cygcb"
station_cfg.pwd="cy123456"
wifi.sta.config(station_cfg)
wifi.sta.connect()
wifi.sta.autoconnect(1)
print(wifi.sta.getip())


tmr.alarm(1, 1000, tmr.ALARM_AUTO, function() 
  if wifi.sta.getip() == nil then 
    print("Waiting for IP ...") 
  else 
    print("IP is " .. wifi.sta.getip())
    tmr.stop(1) 
  end
end)