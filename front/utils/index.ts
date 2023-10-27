export function removeTimeFromDate(date: Date): Date {
    return new Date(date.toDateString());
}
