

-- init wifi connection
dofile('wifi.lua')
-- init led
dofile('led.lua')
-- Serving static files
dofile('httpServer.lua')


httpServer:listen(80)
-- Custom API
-- Get text/html
httpServer:use('/welcome', function(req, res)
    res:send('Hello ' .. req.query.name) -- /welcome?name=doge
end)

httpServer:use('/led', function(req, res)
    --res:send('Control Led ' .. req.query.pin) -- /welcome?name=doge
    if(req.query.pin == "ON1")then
        gpio.write(led1, gpio.LOW);
        print("led1 ON")
    elseif(req.query.pin == "OFF1")then
        gpio.write(led1, gpio.HIGH);
        print("led1 OFF")
    elseif(req.query.pin == "ON2")then
        gpio.write(led2, gpio.LOW);
        print("led2 ON")
    elseif(req.query.pin == "OFF2")then
        gpio.write(led2, gpio.HIGH);
        print("led2 OFF")
    end
    --res:redirect('index.html')
end)

-- Get file
httpServer:use('/doge', function(req, res)
    res:sendFile('doge.jpg')
end)

-- Get json
httpServer:use('/json', function(req, res)
    res:type('application/json')
    res:send('{"doge": "smile"}')
end)

-- Redirect
httpServer:use('/redirect', function(req, res)
    res:redirect('doge.jpg')
end)


