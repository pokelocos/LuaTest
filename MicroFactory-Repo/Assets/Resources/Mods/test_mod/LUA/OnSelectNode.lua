local OnSelectNode = function ()
    MF.AddMoney(-20);
    x = MF.GetMousePosX();
    y = MF.GetMousePosY();
    ParticleManager.SpanwNumberParticle("money",x,y,-20.0);
end

return OnSelectNode