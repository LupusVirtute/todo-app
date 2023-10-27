<script lang="ts" setup>
import type Todo from "~/interfaces/Todo";
import { removeTimeFromDate } from "~/utils";
import { $fetch } from "ofetch";

const config = useRuntimeConfig();
let todos = ref<Todo[]>([]);
let latestTodoTasks = ref<Todo[]>([]);
let date = removeTimeFromDate(new Date());

async function onDateUpdated(event: any) {
    date = new Date(event.target.value);
    pullDataFromDate(removeTimeFromDate(date));
}

async function pullDataFromDate(date: Date) {
    const tomorrow = new Date(date);
    tomorrow.setDate(tomorrow.getDate() + 1);
    const headers = {
        Accept: "application/json",
        "content-type": "application/json",
    };

    const response = await $fetch<Todo[]>(`/Todo`, {
        params: {
            from: date.toDateString(),
            to: tomorrow.toDateString(),
        },
        baseURL: config.public.API_ENDPOINT,
        method: "GET",
        headers,
    });
    todos.value = [];

    todos.value = response as Todo[];

    const latestTodoTasksRequest = await $fetch<Todo[]>(`/todo/latest`, {
        baseURL: config.public.API_ENDPOINT,
        method: "GET",
        headers,
    });

    latestTodoTasks.value = latestTodoTasksRequest as Todo[];
}

const defaultTodo = {
    id: NaN,
    title: "",
    description: "",
    todoDate: new Date(),
};

async function onItemModified() {
    pullDataFromDate(removeTimeFromDate(date));
}

onMounted(() => {
    date = removeTimeFromDate(new Date());
    pullDataFromDate(removeTimeFromDate(new Date()));
});
</script>

<template>
    <div class="grid gap-4 text-white">
        <h1 class="text-2xl text-center mb-24">Todo app</h1>
        <div class="grid gap-x-16 grid-cols-[6fr,1fr]">
            <div class="grid gap-4">
                <div class="max-h-24 w-48">
                    <Input
                        name="date"
                        type="date"
                        :value="date"
                        @input="onDateUpdated"
                    >
                        Data
                    </Input>
                </div>
                <Todo
                    :todo="defaultTodo"
                    @form-submit="onItemModified"
                    :isNew="true"
                />
                <Todos
                    @on-item-modified="onItemModified"
                    v-bind:todos="todos"
                />
            </div>

            <div class="grid gap-4 justify-start self-start">
                <h1>Todo które mają godzinę na wykonanie</h1>
                <div class="grid justify-start gap-2">
                    <div
                        v-for="latestTodoTask in latestTodoTasks"
                        :key="latestTodoTask.id"
                    >
                        <LatestTask :todo="latestTodoTask" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
