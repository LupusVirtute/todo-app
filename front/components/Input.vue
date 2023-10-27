<script setup>
const props = defineProps({
    type: String,
    name: String,
    value: {
        type: [String, Date],
        default: "",
        required: false,
    },
});
</script>

<template>
    <div v-if="type != 'textarea'" class="grid grid-rows-[1fr,3fr] gap-2">
        <label :for="name" class="after:content-[':']">
            <slot />
        </label>
        <input
            :type="type"
            :value="value"
            class="text-white bg-gray-950 h-3/4"
            @input="$emit('update:modelValue', $event.target.value)"
            :name="name"
        />
    </div>
    <div v-else class="grid gap-2 grid-rows-[1fr,3fr]">
        <label :for="name" class="after:content-[':']">
            <slot />
        </label>
        <textarea
            :type="type"
            :value="value.toString()"
            class="text-white bg-gray-950 resize-none h-3/4"
            @input="$emit('update:modelValue', $event.target.value)"
            :name="name"
        />
    </div>
</template>
