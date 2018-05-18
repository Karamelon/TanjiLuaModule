<p align="center"> 
   <img src="https://i.imgur.com/tj6dkVe.png">
</p>

# TanjiLua Module
A Tanji module to give **LUA** support! Make scripts **fast, simple and in real time**.

# Tutorial
The key of this module is simplicity of create own scripts. **Lua** is very simple to learn and coding, to more information how to create scripts  in lua visit  [lua official manual](https://www.lua.org/manual/5.3/).
## GUI
> **GUI Estruture** 
> ```lua 
> -- Create GUI
> Gui:create("Window Name", Width, Height)
> -- Create Button
> Gui:addButton("UniqueButtonName", "Button Text", X,Y)
> -- Create Checkbox
> Gui:addCheckbox("UniqueCheckboxName", "Chackbox text", X,Y)
> -- Create Label 
> Gui:addLabel("UniqueLabelName", "Label text", X,Y)
> -- Create Text Input
> Gui:addInput("UniqueInputName", X,Y)
> -- Show GUI
> Gui:show()
> ```
> **GUI events**
> ```lua 
> -- Handle button event
>function button_UniqueButtonName_click()
>end
> --Handle checkbox event
>function checkbox_UniqueCheckboxName_click(cheked)
>end
> ```
> **Getting GUI values**
> ```lua
> -- Returns a string value
> Gui:getValue(GUIElementUniqueName)
> -- Set Gui text
> Gui:setValue(GUIElementUniqueName, Text)
> -- Getting chebox value
> Gui:isCheked(CheckboxUniqueName)
> ```


## Packets
> **Bind packets events** (out / in)
> ```lua 
> --Bind outgoing value 
> Server:register(packetNumber)
> --Bind incoming value 
> Client:register(packetNumber)
> ```
> 
> **Send packets** (out / in)
> ```lua 
> -- Send outgoing value 
> Server:send(header, {"data", 1, 5, ...})
> --Send incoming value 
> Client:send(header, {"data", 1, 5, ...})
> ```
> **Intercept packets** (out / in)
> Using **Incercept**
> ```lua 
> 
> -- This function is called every time if server message received.
> -- IMPORTANT: server message needs to be registered before.
> 
> function ServerMessageHandle(header, event)
> 
>    -- Intercept server message data.
>    local result = Intercept:data(event, {
>                        Intercept:STRING(), -- Packet read String [1]
>                        Intercept:INT(), --Packet read Int [2]
>                        Intercept:SHORT(), -- Packet read short [3]
>                        Intercept:BOOL(), -- Packet read Boolean [4]
>                        ...
>                     }) 
>                 
>    -- Example of data received access
>    print (result[1]) -- Index depends directly a quantity of poped values
> end
> 
> -- This function is called every time if client message received.
> -- IMPORTANT: server message needs to be registered before
> 
> function ClientMessageHandler(header, event)
>    -- Intercept client message data.
>    local result = Intercept:data(event, {
>                        Intercept:STRING(), -- Packet read String [0]
>                        Intercept:INT(), --Packet read Int [1]
>                        Intercept:SHORT(), -- Packet read short [2]
>                        Intercept:BOOL(), -- Packet read Boolean [3]
>                        ...
>                     }) 
>                 
>    -- Example of data received access
>    print (result[index]) -- Index depends directly a quantity of poped values
> end
> ```

## Util
> ```lua 
> -- MessageBox (Open default MessageBox)
> msgBox("Title", "Content")
> --Print in console
> print("Data")
> ```

## Script Example
> ```lua 
> --Create a simple GUI
> Gui:Create("Say user ID module", 200, 75)
> -- Create a Checkbox
> Gui:AddCheckBox("ckbox", "Say?" , 60, 8)
> -- Show gui
> Gui:Show()
> 
> -- Bind Server Message
> Server:register(2220)
> 
> -- On server message recived
> function ServerMessageHandler(hader, event)
>      -- Check header and verify if chackbox is checked
>      if (header == 2220 and Gui:isCheked("ckbox")) then
>            -- Intercept data, and  store value in local variable
>            local data = Intercept:data(event, {Intercept:INT()})
>            -- Send server message with data
>            Server:send(395, {"ID: "..data[1], 0, 17})
>      end            
>end
> ```
