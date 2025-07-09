using VContainer;

public static class VContainerExtensions {
    public static T InjectNewObject<T>(this IObjectResolver objectResolver) where T : new() {
        T obj = new();
        objectResolver.Inject(obj);
        return obj;
    }
}
