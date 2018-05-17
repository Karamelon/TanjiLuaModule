
Module:name("Auto adm")
Module:author("...")
Module:Description("...")

--Interface
Gui:create("Adm join", 100, 50)
Gui:button("getAdm", "Pegar adm", 20, 10)

local GetUserIdHeader = "RequestWearingBadges"
local GroupSetAdmHeader = "GuildSetAdmin"
local GetGroupIdHeader = "499eef89e0da2f29c486c41222808b5f"

-- Register handlers
Outgoing:register(GetUserIdHeader)
Outgoing:register(GetGroupIdHeader)

local actualUser = nil
local actualGroup = nil

-- Handle Server messages
function ServerMessageHandler(event)
	if event:header = GetUserIdHeader then
		
		actualUser = event:getDataInt(0)
	else if event:header = GetGroupIdHeader then
		actualGroup = event:getDataInt(0)
	end
end

-- Handle Client messages
function ClientMessageHandler(event)

end

-- Define button click event (GUI)
function button_getAdm_click()
	Outgoing:send(GroupSetAdmHeader, actualGroup, actualUser)
	msgBox("Sucesso!", "...")
end






