# Fake GPT – C# Console & SSE ChatGPT Clone

This is a minimal AI chat application built in **C#/.NET**, inspired by ChatGPT. It demonstrates **real-time streaming**, conversation memory, and easy frontend integration.

---

## 🔹 Features

* **Streams responses token by token** (like ChatGPT) for real-time feedback
* **Maintains conversation history** (user + assistant messages)
* **Colored console output** for readability
* **SSE (Server-Sent Events) endpoint** for real-time web frontend integration
* **Bootstrap frontend demo** with:

  * Input box and send button
  * Loading spinner / typing indicator
  * Scrollable chat window with colored badges
* **OpenAPI / Swagger / ReDoc support** for API documentation
* Built on **.NET 6+ / .NET 10 preview** with minimal, reusable code

---

## 🚀 Getting Started

### 1. Clone the repo

```bash
git clone https://github.com/yourusername/fake-gpt.git
cd fake-gpt
```

### 2. Install dependencies

```bash
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

### 4. Run the console app

```bash
dotnet run --project FakeGPT.Console
```

### 5. Run the SSE API (optional)

```bash
dotnet run --project FakeGPT.Api
```

Open `index.html` in a browser to test the **real-time streaming frontend**.

---

## 🖼️ Example Usage

**Console App:**

```
Fake GPT
Simple ChatGPT Console (type 'bye' to quit)

You: Hello!
AI: Hi there! How can I help you today?
```

**Browser SSE Frontend:**

* Type a message in the input box
* Click **Send**
* AI responds **token by token** with a **loading spinner**
* Scrollable chat shows both **user** and **AI messages** with colored badges

---

## 🛠️ Next Steps / Ideas

* Save conversations to file or database
* Add CLI options for model selection
* Extend to **Angular / React frontend** using **SignalR** or **Server-Sent Events (SSE)**
* Customize AI system messages to create different personalities
* Add authentication and rate limiting for production deployments

---

## 📜 License

MIT License. Feel free to use, modify, and share.
