

print('MAC: '..wifi.ap.getmac())
-- init led
dofile('led.lua')
-- Serving static files
dofile('httpServer.lua')
-- init wifi connection
--dofile('wifi.lua')
dofile('apMode.lua')
-- start network monitor
dofile('networkmonitor.lua')

-------------
-- http
-------------
--dofile('httpServer.lua')

httpServer:use('/config', function(req, res)
    if req.query.ssid ~= nil and req.query.pwd ~= nil then
        sta_cfg={}
        sta_cfg.ssid=req.query.ssid
        sta_cfg.pwd=req.query.pwd
        wifi.sta.config(sta_cfg)
        print('connecting to ssid '..sta_cfg.ssid..'...')
        wifi.setmode(wifi.STATIONAP)
        wifi.sta.connect()
        wifi.sta.autoconnect(1)
        status = 'STA_CONNECTING'
        TMR_WIFI = 4
        tmr.alarm(TMR_WIFI, 1000, tmr.ALARM_AUTO, function()
            if status ~= 'STA_CONNECTING' then
                res:type('application/json')
                res:send('{"status":"' .. status .. '"}')
                tmr.stop(TMR_WIFI)
            end
        end)
    end
end)

httpServer:use('/scanap', function(req, res)
    wifi.sta.getap(function(table)
        local aptable = {}
        for ssid,v in pairs(table) do
            local authmode, rssi, bssid, channel = string.match(v, "([^,]+),([^,]+),([^,]+),([^,]+)")
            aptable[ssid] = {
                authmode = authmode,
                rssi = rssi,
                bssid = bssid,
                channel = channel
            }
        end
        res:type('application/json')
        res:send(sjson.encode(aptable))
    end)
end)
