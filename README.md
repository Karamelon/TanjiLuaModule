<p align="center"> 
   <img src="https://i.imgur.com/tj6dkVe.png">
</p>

# TanjiLua Module
A Tanji module to give **LUA** support! Make scripts **fast, simple and in real time**.

# Tutorial

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
>function button_UniqueCheckboxName_click(cheked)
>end
> ```


## Packets
> **Bind packets events** (out / in)
> ```lua 
> --Bind outgoing value 
> Outgoing:register(packetNumber)
> --Bind incoming value 
> Incoming:register(packetNumber)
> ```
> 
> **Send packets** (out / in)
> ```lua 
> -- Send outgoing value 
> Outgoing:send(header, {"data", 1, 5, ...})
> --Send incoming value 
> Incoming:send(header, {"data", 1, 5, ...})
> ```
> **Intercept packets** (out / in)
> Using **Incercept**
> ```lua 
> -- This function is called every time if server message received.
> -- IMPORTANT: server message needs to be registered before.
> function ServerMessageHandle(header, event)
>    -- Intercept server message data.
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
> -- This function is called every time if client message received.
> -- IMPORTANT: server message needs to be registered before
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
> ``

## Script Example
> ```lua 
> 
> ``
