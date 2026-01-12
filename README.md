# Restaurants API

## 概要

レストランと料理を管理するためのASP.NET Core Web APIアプリケーションです。

## 前提条件

- [Visual Studio Code](https://code.visualstudio.com/)
- [.NET SDK](https://dotnet.microsoft.com/download) (バージョン6.0以上)
- [Docker Desktop](https://www.docker.com/get-started)

## 開発環境のセットアップ

### 1. .NET SDK のインストール

公式サイトから最新の .NET SDK をダウンロードしてインストールしてください：

[https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

インストール後、ターミナルで以下のコマンドを実行してバージョンを確認：

```bash
dotnet --version
```

### 2. Visual Studio Code のセットアップ

#### VS Code のインストール

[https://code.visualstudio.com/](https://code.visualstudio.com/) からダウンロードしてインストールしてください。

#### 推奨拡張機能のインストール

以下の拡張機能をインストールしてください：

1. **C# Dev Kit** (必須)
   - [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
   - C#の開発に必要な機能（IntelliSense、デバッグ、リファクタリングなど）を提供

2. **SQL Server (mssql)** (推奨)
   - [SQL Server (mssql)](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)
   - VS Code内でSQL Serverに接続してクエリを実行できます

インストール方法：
- VS Codeを開く
- サイドバーの拡張機能アイコンをクリック（または `Ctrl+Shift+X` / `Cmd+Shift+X`）
- 上記の拡張機能名で検索してインストール

### 3. Docker Desktop のインストール

[https://www.docker.com/get-started](https://www.docker.com/get-started) からDocker Desktopをダウンロードしてインストールしてください。

インストール後、Docker Desktopを起動し、ターミナルで以下のコマンドを実行して確認：

```bash
docker --version
docker-compose --version
```

## プロジェクトのセットアップ手順

### 1. リポジトリのクローン

```bash
git clone <repository-url>
cd Restaurants
```

### 2. Entity Framework Core CLI のインストール

まだインストールしていない場合は、以下のコマンドでインストールしてください：

```bash
dotnet tool install --global dotnet-ef
```

### 3. データベース（SQL Server）の起動

Docker Composeを使用してSQL Serverコンテナを起動します：

```bash
docker-compose up -d
```

コンテナが正常に起動したことを確認：

```bash
docker-compose ps
```

`restaurants-sqlserver` コンテナが `running` 状態であることを確認してください。

### 4. データベースマイグレーションの実行

データベーススキーマを作成するために、マイグレーションを実行します：

```bash
# プロジェクトのルートディレクトリで実行
# データベースを更新（既存のマイグレーションを適用）
dotnet ef database update --project Restaurants.Infrastructure --startup-project Restaurants.API
```

> **注意**: `DbContext`は`Restaurants.Infrastructure`プロジェクトにあるため、`--project`オプションでInfrastructureプロジェクトを指定する必要があります。

### 5. アプリケーションの起動

```bash
# ルートディレクトリに戻る
cd ..

# APIプロジェクトを実行
dotnet run --project Restaurants.API
```

アプリケーションは `https://localhost:5001` または `http://localhost:5000` で起動します。

## データベース接続情報

- **Server**: localhost,1433
- **Database**: RestaurantsDb
- **User**: sa
- **Password**: YourStrong@Passw0rd

> **注意**: 本番環境では、パスワードを環境変数や設定ファイルで管理してください。

## VS CodeでSQL Serverに接続する

SQL Server (mssql) 拡張機能を使用して、VS Code内でデータベースに接続し、クエリを実行できます。

### 1. SQL Server拡張機能でデータベースに接続

1. VS Codeのアクティビティバーから **SQL Server** アイコンをクリック(アイコンなければ一番下の省略されているところから開く)
2. **Add Connection** (接続の追加) ボタンをクリック
3. 以下の情報を入力：
   - **Server name**: `localhost,1433`
   - **Database name** (オプション): `RestaurantsDb`
   - **Authentication Type**: `SQL Login`
   - **User name**: `sa`
   - **Password**: `YourStrong@Passw0rd`
   - **Save Password**: `Yes` (パスワードを保存する場合)
   - **Profile Name** (オプション): `Restaurants DB` (任意の名前)

4. 接続が成功すると、サイドバーにデータベースが表示されます

### 2. クエリの実行

#### 方法1: SQL Serverサイドバーから

1. サイドバーの接続を展開
2. データベース → テーブルを展開
3. テーブルを右クリック → **Select Top 1000** を選択

#### 方法2: SQLファイルから実行

1. 新しいファイルを作成（例: `test.sql`）
2. SQLクエリを記述：
   ```sql
   USE RestaurantsDb;

   -- レストラン一覧を取得
   SELECT * FROM Restaurants;

   -- 料理一覧を取得
   SELECT * FROM Dishes;
   ```
3. ファイル内で右クリック → **Execute Query** を選択
4. 接続プロファイルを選択（先ほど作成した `Restaurants DB` など）
5. 結果がエディタ内に表示されます

### 3. よく使う機能

- **テーブル構造の確認**: テーブルを展開して、カラム名と型を確認
- **データの追加・更新**: INSERT/UPDATE文を記述して実行
- **スキーマの確認**: データベースを右クリック → **Generate Script** でスキーマをエクスポート

### トラブルシューティング

接続できない場合：
1. Docker Composeでコンテナが起動しているか確認
2. サーバー名が `localhost,1433` になっているか確認（カンマに注意）
3. パスワードが正しいか確認

## 便利なコマンド

### Docker Composeコンテナの停止

```bash
docker-compose down
```

### Docker Composeコンテナの停止とボリューム削除（データベースをリセット）

```bash
docker-compose down -v
```

### 新しいマイグレーションの追加

```bash
# プロジェクトのルートディレクトリで実行

# 1. マイグレーションファイルを作成
dotnet ef migrations add <MigrationName> --project Restaurants.Infrastructure --startup-project Restaurants.API

# 2. データベースにマイグレーションを適用
dotnet ef database update --project Restaurants.Infrastructure --startup-project Restaurants.API
```

#### オプションの説明

| オプション | 説明 |
|-----------|------|
| `--project` | マイグレーションファイルが作成されるプロジェクト（`DbContext`がある場所） |
| `--startup-project` | 接続文字列などの設定を読み込むプロジェクト（`appsettings.json`がある場所） |

#### 例：Dishesテーブルにカロリーカラムを追加

```bash
dotnet ef migrations add AddKiloCaloriesToDishes --project Restaurants.Infrastructure --startup-project Restaurants.API
dotnet ef database update --project Restaurants.Infrastructure --startup-project Restaurants.API
```

### データベースのリセット

```bash
# プロジェクトのルートディレクトリで実行
dotnet ef database drop --project Restaurants.Infrastructure --startup-project Restaurants.API
dotnet ef database update --project Restaurants.Infrastructure --startup-project Restaurants.API
```

### マイグレーションの削除（最後のマイグレーションを取り消す）

```bash
# 最後のマイグレーションを削除（まだデータベースに適用されていない場合）
dotnet ef migrations remove --project Restaurants.Infrastructure --startup-project Restaurants.API
```

### マイグレーション一覧の確認

```bash
dotnet ef migrations list --project Restaurants.Infrastructure --startup-project Restaurants.API
```

## トラブルシューティング

### SQL Serverコンテナに接続できない

1. コンテナが起動していることを確認：
   ```bash
   docker-compose ps
   ```

2. コンテナのログを確認：
   ```bash
   docker-compose logs sqlserver
   ```

3. ヘルスチェックの状態を確認：
   ```bash
   docker inspect restaurants-sqlserver --format='{{.State.Health.Status}}'
   ```

### マイグレーションエラー

- Entity Framework Core CLIツールがインストールされているか確認
- 接続文字列が正しいか確認
- SQL Serverコンテナが起動しているか確認
