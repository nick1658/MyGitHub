-- A simple http client
conn=net.createConnection(net.TCP, 0) 
conn:on("receive", function(conn, pl) print(pl) end)
conn:connect(80,"121.41.33.127")
conn:send("GET / HTTP/1.1\r\nHost: www.baidu.com\r\n"
    .."Connection: keep-alive\r\nAccept: */*\r\n\r\n")