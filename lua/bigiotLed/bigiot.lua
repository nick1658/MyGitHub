--bigiot.lua

--here SSID and PassWord should be modified according your wireless router

if wifi.sta.getip()== nil then 
    print("set up wifi mode")
    wifi.setmode(wifi.STATION)
    station_cfg={}
    station_cfg.ssid="cygcb"
    station_cfg.pwd="cy123456"
    wifi.sta.config(station_cfg)
    wifi.sta.autoconnect(1)
    wifi.sta.connect()
end
tmr.alarm(1, 1000, 1, function()
if wifi.sta.getip()== nil then
    print("IP unavaiable, Waiting...")
else
    tmr.stop(1)
    print("Config done, IP is "..wifi.sta.getip())
    dofile("kaiguan.lua")
end
end)