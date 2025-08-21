# Fake GPT – C# Console ChatGPT Clone  

This is a **minimal console application in C#** that works like ChatGPT, built using the official [OpenAI .NET SDK](https://www.nuget.org/packages/OpenAI/).  

🔹 Features:  
- Streams responses **token by token** (like ChatGPT)  
- Maintains **conversation history** (user + assistant messages)  
- Uses **colored console output** for readability  
- Built on **.NET 6+** with less than 100 lines of code  

---

## 🚀 Getting Started  

### 1. Clone the repo  
```sh
git clone https://github.com/yourusername/fake-gpt.git
cd fake-gpt
```

### 2. Install dependencies  
```sh
dotnet add package OpenAI --version 2.3.0
```

### 3. Set your API key (recommended via environment variable)  

**Windows (PowerShell):**
```powershell
setx OPENAI_API_KEY "sk-proj-xxxxx"
```

**Linux/Mac (bash/zsh):**
```bash
export OPENAI_API_KEY="sk-proj-xxxxx"
```

### 4. Run the app  
```sh
dotnet run
```

---

## 🖼️ Example Usage  

```
Fake GPT
Simple ChatGPT Console (type 'bye' to quit)

You: Hello!
AI: Hi there! How can I help you today?
```

---

## 🛠️ Next Steps  

- Save conversations to file or database  
- Add CLI options for model selection  
- Extend to **Angular/React front-end** with **SignalR / Server-Sent Events (SSE)** on **.NET 10**  

---

## 📜 License  

MIT License. Feel free to use, modify, and share.  
