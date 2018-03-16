
DEVICEID = "4805"
APIKEY = "c06b00ffc"
INPUTID = "4304"
host = host or "www.bigiot.net"
port = port or 8181
LED = 4
gpio.mode(LED,gpio.OUTPUT)
gpio.write(LED, gpio.LOW)
local function run()

  tmr.stop(6)
  ok, s = pcall(sjson.encode, {M="checkin",ID=DEVICEID,K=APIKEY})
  if ok then
    print(s)
  else
    print("failed to encode!")
  end
 
  local cu = net.createConnection(net.TCP,0)
  cu:on("receive", function(cu, c) 
    print("receive: "..c)
    r = sjson.decode(c)
    if r.M == "say" then
      if r.C == "play" then   
        gpio.write(LED, gpio.LOW)  
        print('led on')
        ok, played = pcall(sjson.encode, {M="say",ID=r.ID,C="LED turn on!"})
        cu:send( played.."\n" )
      end
      if r.C == "stop" then   
        gpio.write(LED, gpio.HIGH)
        print('led off')
        ok, stoped = pcall(sjson.encode, {M="say",ID=r.ID,C="LED turn off!"})
        cu:send( stoped.."\n" ) 
      end
    elseif r.M == "WELCOME TO BIGIOT" then
    end
  end)
  cu:on("disconnection",function(scu)
    cu = nil
    --停止心跳包发送定时器，5秒后重试
    tmr.stop(1)
    tmr.alarm(6, 5000, 0, run)
  end)
  
  
  cu:on("connection", function(sck, str)
      -- 'Connection: close' rather than 'Connection: keep-alive' to have server 
      -- initiate a close of the connection after final response (frees memory 
      -- earlier here), https://tools.ietf.org/html/rfc7230#section-6.6 
      if str ~= nil then
        print("connection: "..str)
      end
      print("send: "..s)
      cu:send(s.."\n")
  end)

  tmr.alarm(1, 30000, 1, function()
    cu:send(s.."\n")
  end)
  
  cu:connect(port, host)
end
run()
