<script setup lang="ts">
import type Todo from "~/interfaces/Todo";

interface Props {
    todo: Todo;
}

const props = defineProps<Props>();

const { todo } = toRefs(props);
</script>

<template>
    <div class="grid bg-gray-900 px-2 py-4 gap-2">
        <div class="grid bg-gray-950 py-2 px-4">
            <span>Tytuł:</span>
            <span>{{ todo.title }}</span>
        </div>
        <div class="grid bg-gray-950 py-2 px-4">
            <span>Opis:</span>
            <span>{{ todo.description }}</span>
        </div>
        <div class="bg-gray-950 py-2 px-4">
            <span>Koniec Todo: </span>
            <span>{{ new Date(todo.todoDate).toLocaleString() }}</span>
        </div>
    </div>
</template>
