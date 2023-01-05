using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ChatGPTInteractor;

public partial class Form1 : Form
{
    private (int x, int y) _inputCursorPosition;
    private (int x, int y) _clearConversationCursorPosition;
    
    public Form1()
    {
        InitializeComponent();
        
        //Make sure you have the browser open, with chatGPT open, and the cursor is in the input box
        
        //TODO: make buttons to allow user to set mouse cursor x,y for chatGPT input text box and clear conversations button.
        //This will allow a continuous chat session (currently need to refresh via F5)
        //_inputCursorPosition=(Cursor.Position.X, Cursor.Position.Y);
        //_clearConversationCursorPosition=(Cursor.Position.X, Cursor.Position.Y);
        
        var responseText=StartChatGPTGrabber("Hello!");
        
        MessageBox.Show(responseText);
    }

    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    
    private void BringToFront(Process pTemp)
    {
        SetForegroundWindow(pTemp.MainWindowHandle);
    }
    
    private string StartChatGPTGrabber(string prompt)
    {
        var browserName = "brave";
        var process = Process.GetProcessesByName(browserName);
        var ignoreText=$"**ignore this {Guid.NewGuid().ToString().ToUpperInvariant()}**";

        Clipboard.Clear();

        BringToFront(process[0]);

        SendKeys.SendWait(prompt+ " " + ignoreText);
        SendKeys.SendWait("{ENTER}");
        SendKeys.SendWait("{TAB}");

        var text = Clipboard.GetText();

        while (!text.Contains("Regenerate" + " response"))
        {
            Thread.Sleep(1000);
            
            BringToFront(process[0]);

            SendKeys.SendWait("^a");
            SendKeys.SendWait("^c");

            text = Clipboard.GetText();
        }
        
        //Remove to have continuous chat session, or enable to start a new session.
        SendKeys.SendWait("{F5}");

        var startIndex = text.LastIndexOf(ignoreText);
        var endIndex = text.IndexOf("Regenerate response");
        var responseText = text.Substring(startIndex, endIndex-startIndex).Replace(ignoreText,"").Trim();

        return responseText;
    }
}