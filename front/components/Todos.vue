<script setup lang="ts">
import type Todo from "~/interfaces/Todo";
interface Props {
    todos: Todo[];
}

const emit = defineEmits(["onItemModified"]);

const props = defineProps<Props>();
</script>

<template>
    <div>
        <div :key="todo.id" v-for="todo in todos">
            <Todo
                @on-item-modified="() => emit('onItemModified')"
                @item-updated="() => emit('onItemModified')"
                @deleted-item="() => emit('onItemModified')"
                :todo="todo"
                :isNew="false"
            />
        </div>
    </div>
</template>
