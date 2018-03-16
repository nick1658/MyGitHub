
--pwm.setup(1, 500, 512)
pwm.setup(4, 500, 512)
--pwm.setup(3, 500, 512)
--pwm.start(1)
pwm.start(4)
--pwm.start(3)
function led(r, g, b)
    --pwm.setduty(1, g)
    pwm.setduty(4, b)
   -- pwm.setduty(3, r)
end
--led(512, 0, 0) --  set led to red
led(0, 0, 1000) -- set led to blue.