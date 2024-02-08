import {useRouteError} from "react-router-dom";

export default function ErrorPage() {
    const error = useRouteError();
    console.error(error);
    
    return (
        <div id="error-page" className="flex flex-col space-y-2 items-center justify-center h-full">
            <h1 className="text-6xl">Ой!</h1>
            <p className="text-xl text-muted-foreground">Извините, произошла ошибка.</p>
            <p className="text-lg">
                {/*@ts-ignore*/}
                <i>{error.statusText || error.message || error.statusCode}</i>
            </p>
        </div>
    );
}