local OnChangeTimeSpeed = function ()
    MF.AddMoney(-100);
    x = MF.GetMousePosX();
    y = MF.GetMousePosY();
    ParticleManager.SpanwNumberParticle("money",x,y,-100.0);
end

return OnChangeTimeSpeed