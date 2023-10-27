<script setup lang="ts">
import type { VNodeRef } from "vue";
import type Todo from "~/interfaces/Todo";

interface Props {
    isNew: boolean;
    todo: Todo;
}

const props = defineProps<Props>();
const config = useRuntimeConfig();
const fullPostUrl = `${config.public.API_ENDPOINT}/Todo`;
const emit = defineEmits(["formSubmit", "deletedItem", "itemUpdated"]);

const form = ref<VNodeRef | null>(null);

function submit() {
    if (!form) {
        console.log("Form wasn't found");
        return;
    }

    const request = new XMLHttpRequest();

    request.open("POST", fullPostUrl, true);
    // @ts-ignore
    request.send(new FormData(form.value as HTMLFormElement));
    emit("formSubmit");
}

async function onEdit() {
    if (props.isNew) {
        return;
    }

    const formData = new FormData(form.value as HTMLFormElement);

    const newTodo = {
        id: props.todo.id,
        title: formData.get("title"),
        description: formData.get("description"),
        todoDate: formData.get("TodoDate"),
    };

    await useFetch(`${fullPostUrl}/edit`, {
        method: "POST",
        body: {
            todo: newTodo,
        },
    });

    emit("itemUpdated");
}

async function onDelete() {
    if (props.isNew) {
        return;
    }
    await useFetch(`${fullPostUrl}/${props.todo.id}`, {
        method: "delete",
    });
    emit("deletedItem");
}
</script>
<template>
    <div>
        <form
            class="grid gap-2 items-center justify-center grid-cols-5"
            method="post"
            ref="form"
            :action="fullPostUrl"
            @submit.prevent
        >
            <Input v-model:value="todo.title" name="title" type="text">
                Tytuł
            </Input>
            <Input
                v-model:value="todo.description"
                name="description"
                type="textarea"
            >
                Opis
            </Input>
            <Input
                v-model:value="todo.todoDate"
                name="TodoDate"
                type="datetime-local"
            >
                Data
            </Input>
            <div v-if="isNew" class="h-1/2 mt-4 grid gap-2">
                <button @click="submit" class="bg-gray-950">Dodaj</button>
            </div>
            <div v-else class="h-1/2 mt-4 grid gap-2 grid-cols-2">
                <button @click="onEdit" class="bg-gray-950">Edytuj</button>
                <button @click="onDelete" class="bg-gray-950">Usuń</button>
            </div>
        </form>
    </div>
</template>
