# chatGPT-Interactor
Interact with chatGPT in a browser using this client.  It sends keyboard commands to send messages and copy replies.  A simple winforms app, useful for creating an api, mqtt client, etc for chatGPT since none exists that's easy to use.

## Instructions
1. Make sure you have the browser open, with chatGPT open, and the cursor is in the input box
2. Open the project and run it OR run the exe in the bin folder
3. You'll see "Hello!" get entered into the chatGPT window input box with results copied to clipboard.
4. A message box will pop up with the chatGPT response.

## Notes
* The program will wait a second after sending the message and before copying the response to clipboard.  This is to give chatGPT time to respond.  If you want to change this, change the `Thread.Sleep(1000);` line in the `SendKeys.SendWait("{ENTER}");` line.
* It currently only copies the response, then refreshes to create a new session.  If you want a continuous chat session, you need to use the buttons to set mouse cursor coordinates for the input box...  I'll add this later.
* I connected this to mqtt and used it to create a chatbot / general ai tool for an app I made.. as an example of how this can be used.  You can also run it along an api and call it.  
* I chose winforms, because it's simple, fast, and not sure if it's even possible in wpf or others. 