using System;

public class EventBus
{
    public event EventHandler<EventBusType> EventChangeEventHandler;
    public event EventHandler<UmsTypeEnum> LoginTypeEventHandler;
    public event EventHandler<EventBusType, string> EditConfigPageOpenEventHandler;
    public event EventHandler<string> DeleteExSystemEventHandler;
    public event EventHandler<string, bool> ExSystemEnableChangeEventHandler;
    public event EventHandler LanguageChangeEventHandler;

    public void Publish(EventBusType eventType, string exSystemName = null, bool CommEnbale = false)
    {
        EventChangeEventHandler?.Invoke(this, eventType);
    }

    public void LoginTypePublish(UmsTypeEnum loginType)
    {
        LoginTypeEventHandler?.Invoke(this, loginType);
    }

    public void EditConfigPageOpenPublish(EventBusType eventType, string pageName)
    {
        EditConfigPageOpenEventHandler?.Invoke(this, eventType, pageName);
    }

    public void DeleteExSystemPublish(string DeletedSystemName)
    {
        DeleteExSystemEventHandler?.Invoke(this, DeletedSystemName);
    }

    public void ExSystemEnableChangePublish(string exSystemName, bool CommEnable)
    {
        ExSystemEnableChangeEventHandler?.Invoke(this, exSystemName, CommEnable);
    }

    public void LanguageChangePublish()
    {
        LanguageChangeEventHandler?.Invoke(this, EventArgs.Empty);
    }
} 