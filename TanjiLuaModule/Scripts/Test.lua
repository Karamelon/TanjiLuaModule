--[[
script {
    script_name("SCRIPT_NAME"),
    script_description("SCRIPT_DESCRIPTION")
}

gui {
    gui_label("ID", "VALUE"),
    gui_button("ID", "VALUE"),
    gui_input("ID","TYPE","VALUE")
}


outPackets {
	2555,
	1234,
	3432,
	1182,
}


inPackets {
	4374,
	2323,
	2323,
	3544,
}
]]

Gui:create("Script tool", 250, 250)
Gui:addButton("frist", "Clique aqui", 10, 10)
Gui:addCheckBox("ckbox", "clique", 100, 100)
Gui:show()


function button_frist_click()
	msgBox("Title", "Botão clicado!")
end

function checkbox_ckbox_click(cheked)
	if cheked then
		msgBox("Caixa", "Checada!")
	end
end

--Incomming:register(123);
--Incomming:register(4001);

Outgoing:register(702);

--Outgoing:send(123, {0,0,1})
--Outgoing::register(3243);
--Outgoing::register("dsdd");


local send = nil
function ServerMessageHandler(header, data)
    --print "ServerEventCall"
	--if header == 395 then
		local datal = Intercept:data(data, {
						"int",
						"str"})
		Outgoing:send(395, {"kkkkkk", 0, 17})
	--end
    -- Generic LUA condition
    --[[
	if (event.header == 2234) then
        
        -- Set GUI label VALUE with event packet value (frist possition).
        
		gui_set_value("INPUT_ID", event.packet[0])
        -- Send server message.
        sendServer(2334, {
            12830928,
            "String",
            0.4
        })

        -- Send client message.
        
		sendClient(2334, {
            12830928,
            "String",
            0.4
        })
    end
	]]
	print ""
end

function ClientMessageHandler(header, data)
	
end


