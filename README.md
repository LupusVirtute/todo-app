# Todo List written in Nuxt 3 with C# ASP.NET CQRS backend

## How to run backend?

1. `docker compose up --build -d`
2. Run backend with Visual Studio

- Optionally use run-backend-debug.sh
- Only use run-backend-debug.sh if you got msbuild.exe in environment variables
- Optionally compile it with visual studio or msbuild and then run it

## How to run frontend

1. Copy .env.example and rename it to .env
2. Change variables of .env to match backend api
3. `yarn install`
4. `yarn dev`
5. Open browser on localhost:3000

## How to see todos for given date?

1. Set date in `Data:` input field
2. Should automatically refresh

## How to add todos

1. Set title
2. Set description
3. Set Todo date
4. Click `Dodaj`

## How to remove todos

1. Select todo you want to remove and click `Usuń`

## How to edit todos

1. Change something in title description or date of given todo and click `Edytuj`

## How tasks in hour works?

1. Task in hour works by calculating every task in an hour

## Troubleshooting

1. Problems with docker?

- Run `fix-docker-scripts.sh` script
