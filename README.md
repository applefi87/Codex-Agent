## 專案說明

此專案為使用 .devcontainer 設定的 C# (.NET) 開發環境配置範本，包含以下內容：

- **.devcontainer/devcontainer.json**：用於設定開發容器的配置檔，基於 Microsoft 的 .NET 8.0 映像檔，並啟用 Azure CLI 與 Node.js 功能套件，支援 Bicep、pnpm、nvm 與 yarn 等工具，並對常用的服務埠 (3000、5000、5001、8000、8080) 開放轉發。
- **AGENTS.md**：提醒使用者以繁體中文 (zh-TW) 進行溝通。
- **a.txt**：空白檔案，留作範例或占位之用。

此範本可作為在 Visual Studio Code 或 GitHub Codespaces 中快速建立符合 .NET 開發需求之容器化環境的起點。
