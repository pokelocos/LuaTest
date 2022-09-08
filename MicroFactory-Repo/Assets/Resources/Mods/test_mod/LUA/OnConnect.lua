local OnConnect = function ()
    MF.AddMoney(-50);
    x = MF.GetMousePosX();
    y = MF.GetMousePosY();
    ParticleManager.SpanwNumberParticle("money",x,y,-50.0);
end

return OnConnect