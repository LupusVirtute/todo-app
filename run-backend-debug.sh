docker compose up --build -d
MsBuild.exe ./back/TodoListBackend/TodoListBackend.sln
cd ./back/TodoListBackend/TodoListBackend/bin/Debug/net7.0/
./TodoListBackend.exe