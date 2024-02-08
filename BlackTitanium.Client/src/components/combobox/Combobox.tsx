import * as React from "react"
import {CaretSortIcon, CheckIcon} from "@radix-ui/react-icons"

import {cn} from "@shadcn/lib/utils"
import {Button} from "@shadcn/components/ui/button"
import {
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
} from "@shadcn/components/ui/command"
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@shadcn/components/ui/popover"

interface ComboboxProps<T> {
    items: T[],
    find: (item: T, value: string) => boolean,
    displayMember: (item: T | undefined) => string | undefined,
    valueMember: (item: T) => string | undefined,
    keyMember: (item: T) => React.Key | null | undefined
}

export function Combobox<T>(props: ComboboxProps<T>) {
    const [open, setOpen] = React.useState(false)
    const [value, setValue] = React.useState("")

    function find(value: string) {
        return props.displayMember(props.items.find(item => props.find(item, value)))
    }

    return (
        <Popover open={open} onOpenChange={setOpen}>
            <PopoverTrigger asChild>
                <Button
                    variant="outline"
                    role="combobox"
                    aria-expanded={open}
                    className="w-[200px] justify-between"
                >
                    {value
                        ? find(value)
                        : "Выберите..."}
                    <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50"/>
                </Button>
            </PopoverTrigger>
            <PopoverContent className="w-[200px] p-0">
                <Command>
                    <CommandInput placeholder="Поиск..." className="h-9"/>
                    <CommandEmpty>Не найдено.</CommandEmpty>
                    <CommandGroup>
                        {props.items.map((item) => (
                            <CommandItem
                                key={props.keyMember(item)}
                                value={props.valueMember(item)}
                                onSelect={(currentValue) => {
                                    setValue(currentValue === value ? "" : currentValue)
                                    setOpen(false)
                                }}
                            >
                                {props.displayMember(item)}
                                <CheckIcon
                                    className={cn(
                                        "ml-auto h-4 w-4",
                                        value === props.valueMember(item) ? "opacity-100" : "opacity-0"
                                    )}
                                />
                            </CommandItem>
                        ))}
                    </CommandGroup>
                </Command>
            </PopoverContent>
        </Popover>
    )
}