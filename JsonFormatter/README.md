# JsonFormatter

此應用程式會讀取專案目錄下的 `test.json` 檔案內容，若為 JSON 陣列則逐項格式化輸出，或支援換行分隔 (ndjson) 的多個 JSON 物件，並將格式化後的 JSON 顯示於主控台。

## 使用方式

```bash
# 請將要格式化的 JSON 檔命名為 test.json 並放置在專案目錄下
dotnet run --project JsonFormatter
```
執行後即可自動讀取並格式化 test.json 中的所有 JSON 物件。