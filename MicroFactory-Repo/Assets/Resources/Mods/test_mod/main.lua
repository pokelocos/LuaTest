local OnStartGame = function ()
    --Debug("Debug ")
    Node.RandomColor()
end

return OnStartGame



local OnConnect = function ()
    MF.AddMoney(-50);
    MF.SpanwParticle("substract_money",50)
end

return OnConnect



local OnDisconnect = function ()
    MF.AddMoney(-50);
    MF.SpanwParticle("substract_money",50)
end

return OnDisconnect



local OnSelectNode = function ()
    MF.AddMoney(-25);
    MF.SpanwParticle("substract_money",25)
end

return OnSelectNode




local OnChangeTimeSpeed = function ()
    MF.AddMoney(-100);
    MF.SpanwParticle("substract_money",100)
end

return OnChangeTimeSpeed