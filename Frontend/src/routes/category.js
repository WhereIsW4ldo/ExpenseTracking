import { writable, derived } from 'svelte/store';

export const apiData = writable([]);

export const categories = derived(apiData, ($apiData) => {
    return $apiData.map(category => [category.id, category.name]);
});