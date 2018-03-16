
--[[
station_cfg={}
station_cfg.ssid="cygcbe"
station_cfg.pwd="cy123456"
wifi.sta.config(station_cfg)--]]
wifi.ap.config({ ssid = 'NodeMCU', pwd = 'nodemcu88' }) cfg={}
--cfg={}
--cfg.ssid="NodeMCU"
--cfg.pwd="nodemcu88"
--wifi.ap.config(cfg)

wifi.setmode(wifi.STATIONAP)
gpio.write(led2, gpio.HIGH)
httpServer:listen(80)