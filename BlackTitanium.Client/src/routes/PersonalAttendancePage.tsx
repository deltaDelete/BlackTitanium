import {Input} from "@shadcn/components/ui/input.tsx";
import {Card, CardContent, CardHeader} from "@shadcn/components/ui/card.tsx";
import {Button} from "@shadcn/components/ui/button.tsx";
import {Label} from "@shadcn/components/ui/label.tsx";
import {
    Select,
    SelectContent,
    SelectGroup,
    SelectItem,
    SelectTrigger,
    SelectValue
} from "@shadcn/components/ui/select.tsx";
import {useState} from "react";

export default function PersonalAttendancePage() {
    const [department, setDepartment] = useState<Department | undefined>()

    return (
        <div className="flex flex-col space-y-4 justify-stretch m-10">
            <h2 className="text-3xl">Форма записи на посещение мироприятия</h2>
            <div className="flex flex-row space-x-4 justify-stretch">
                <Card className="flex-grow overflow-clip space-y-4">
                    <CardHeader className="bg-accent text-accent-foreground rounded-xl">Информация для
                        пропуска</CardHeader>
                    <CardContent className="space-y-2">
                        <Label>Срок дествия заявки</Label>
                        <Input type="date"/>
                        <Input type="date"/>
                        <Select>
                            <SelectTrigger>
                                <SelectValue placeholder="Выберите цель"/>
                            </SelectTrigger>
                            <SelectContent>
                                <SelectGroup>
                                    <SelectItem value={"h"}>Прив</SelectItem>
                                    <SelectItem value={"he"}>Привет</SelectItem>
                                    <SelectItem value={"hel"}>Привет м</SelectItem>
                                    <SelectItem value={"hell"}>Привет мир</SelectItem>
                                </SelectGroup>
                            </SelectContent>
                        </Select>
                    </CardContent>
                </Card>
                <Card className="flex-grow overflow-clip space-y-4">
                    <CardHeader className="bg-accent text-accent-foreground rounded-xl">Принимающая сторона</CardHeader>
                    <CardContent className="space-y-2">
                        <Select onValueChange={value => setDepartment(departments.find(i => i.id.toString() == value))}>
                            <SelectTrigger>
                                <SelectValue placeholder="Выберите отдел"/>
                            </SelectTrigger>
                            <SelectContent>
                                <SelectGroup>
                                    {departments.map(value => {
                                        return <SelectItem value={value.id.toString()}>{value.name}</SelectItem>
                                    })}
                                </SelectGroup>
                            </SelectContent>
                        </Select>
                        <Select>
                            <SelectTrigger>
                                <SelectValue placeholder="Выберите сотрудника"/>
                            </SelectTrigger>
                            <SelectContent>
                                <SelectGroup>
                                    {department?.staff.map(value => <SelectItem
                                        value={value.id.toString()}>{value.lastname} {value.firstname}</SelectItem>)}
                                </SelectGroup>
                            </SelectContent>
                        </Select>
                    </CardContent>
                </Card>
            </div>
            <div className="flex flex-row space-x-4 justify-stretch">
                <Card className="flex-grow overflow-clip space-y-4">
                    <CardHeader className="bg-accent text-accent-foreground rounded-xl">Информация о
                        посетителе</CardHeader>
                    <CardContent>
                        <Input type="date"/>
                    </CardContent>
                </Card>
            </div>
            <div className="flex flex-row space-x-4 justify-stretch">
                <Card className="flex-grow overflow-clip space-y-4">
                    <CardHeader className="bg-accent text-accent-foreground rounded-xl">Приклепляемые
                        документы</CardHeader>
                    <CardContent>
                        <Input type="date"/>
                    </CardContent>
                </Card>
                <Button variant="ghost" size="xl"
                        className="text-muted-foreground hover:text-muted-foreground hover:bg-muted">Очистить
                    форму</Button>
                <Button variant="outline" size="xl" className="border-accent">Оформить заявку</Button>
            </div>
        </div>
    )
}

// Ожидаемый JSON от API
/*
[
    {
        "departmentId": 1,
        "name": "SomeName",
        "staff": [
            // сотрудники
        ]
    }
]
 */
class Department {
    id: number;
    name: string;
    staff: Staff[] = [];

    constructor(id: number, name: string, staff: Staff[]) {
        this.id = id;
        this.name = name;
        this.staff = staff;
    }

    static Empty(id: number, name: string) {
        return new this(id, name, [])
    }

    static Full(id: number, name: string, staff: Staff[]) {
        return new this(id, name, staff)
    }
}

class Staff {
    id: number = -1;
    lastname: string = "";
    firstname: string = "";
}

const departments = [
    Department.Empty(1, "Деп 1"),
    Department.Full(2, "Деп 2", [
        {
            id: 1,
            lastname: "Привет",
            firstname: "Пока"
        }
    ]),
    Department.Empty(3, "Деп 3"),
]