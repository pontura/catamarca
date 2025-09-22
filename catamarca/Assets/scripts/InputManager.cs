using UnityEngine;

public class InputManager : MonoBehaviour
{
    int playerID;
    int inputType = 0;

    public KeyCode key1;
    public KeyCode key2;
    public KeyCode key3;

    public void Init(int playerID)
    {
        inputType = Data.Instance.gameData.data.inputType;
        this.playerID = playerID;
        print("Init " + playerID + " inputType: " + inputType + ": Data.Instance.gameData.data.p1_key1: " + Data.Instance.gameData.data.p1_key1);
        if(playerID == 0)
        {
            key1 = ParseKeyCode(Data.Instance.gameData.data.p1_key1);
            key2 = ParseKeyCode(Data.Instance.gameData.data.p1_key2);
            key3 = ParseKeyCode(Data.Instance.gameData.data.p1_key3);
        }
        else
        {
            key1 = ParseKeyCode(Data.Instance.gameData.data.p2_key1);
            key2 = ParseKeyCode(Data.Instance.gameData.data.p2_key2);
            key3 = ParseKeyCode(Data.Instance.gameData.data.p2_key3);
        }

        print("key1 " + key1);
    }
    private void Update()
    {
        if (inputType == 0) return;
        if (Input.GetKeyDown(key1)) KeyPressed(1);
        if (Input.GetKeyDown(key2)) KeyPressed(2);
        if (Input.GetKeyDown(key3)) KeyPressed(3);
    }
    private KeyCode ParseKeyCode(string s)
    {
        if (System.Enum.TryParse(s, out KeyCode key))
            return key;
        Debug.LogWarning($"No se pudo parsear {s} como KeyCode");
        return KeyCode.None;
    }

    void KeyPressed(int id)
    {
        print("key pressed " + playerID + " id: " +  id);
        Events.OnKeyPressed(playerID, id);
    }
}
